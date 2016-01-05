using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeckLinkAPI;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;

namespace VideoTest
{
  public partial class Form1 : Form, IDeckLinkDeviceNotificationCallback, IDeckLinkInputCallback, 
    IDeckLinkScreenPreviewCallback, IBMDStreamingDeviceNotificationCallback
  {
    private readonly _BMDAudioSampleRate _AudioSampleRate = _BMDAudioSampleRate.bmdAudioSampleRate48kHz;
    private readonly _BMDAudioSampleType _AudioSampleType = _BMDAudioSampleType.bmdAudioSampleType32bitInteger;
    private readonly uint _AudioChannels = 2;
    private readonly uint _AudioSampleDepth = 32;

    private readonly IBMDStreamingDiscovery _BMDDiscovery;
    private readonly IDeckLinkDiscovery _Discovery;
    private readonly IDeckLinkGLScreenPreviewHelper _GLHelper;

    private long _FrameCount = 0;
    private long _PreviewCount = 0;
    private bool _Streaming = false;
    private IDeckLink _DeckLink = null;
    private System.Timers.Timer _GLHack = new System.Timers.Timer();
    private BinaryWriter _VideoWriter = null;
    
    public Form1()
    {
      InitializeComponent();

      _Discovery = new CDeckLinkDiscovery();
      _GLHelper = new CDeckLinkGLScreenPreviewHelper();

      _BMDDiscovery = new CBMDStreamingDiscovery();

      if (_Discovery != null) _Discovery.InstallDeviceNotifications(this);
      if (_BMDDiscovery != null) _BMDDiscovery.InstallDeviceNotifications(this);

      previewBox.Paint += PreviewBox_Paint;

      find.Enabled = false;
      stream.Enabled = false;
      notifications.Text = "Please wait 2 seconds for the preview box to initialise...";

      _GLHack.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
      _GLHack.Elapsed += DelayedLoad;
      _GLHack.AutoReset = false;
    }

    private void DelayedLoad(object sender, System.Timers.ElapsedEventArgs e)
    {
      Run(() => 
      {
        find.Enabled = true;
        stream.Enabled = true;
        SetupPreviewBox();
      });
    }

    private void PreviewBox_Paint(object sender, PaintEventArgs e)
    {
      //_GLHelper.PaintGL();
    }
    
    private void Form1_Load(object sender, EventArgs e)
    {
      _GLHack.Start();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_Streaming)
      {
        MessageBox.Show("Kill the streaming first!");
        e.Cancel = true;
        return;
      }

      if (_Discovery != null) _Discovery.UninstallDeviceNotifications();
      if (_BMDDiscovery != null) _BMDDiscovery.UninstallDeviceNotifications();
    }

    private void Run(Action act)
    {
      Invoke(act);
    }

    private string FormatDevice(string display, string model)
    {
      return display == model ? display : string.Format("{0} ({1})", display, model);
    }

    public void DeckLinkDeviceArrived(IDeckLink deckLinkDevice)
    {
      string displayName;
      string modelName;

      deckLinkDevice.GetDisplayName(out displayName);
      deckLinkDevice.GetModelName(out modelName);
      
      Run(() => notifications.Text = string.Format("Device connected! {0}", FormatDevice(displayName, modelName)));
    }

    public void DeckLinkDeviceRemoved(IDeckLink deckLinkDevice)
    {
      string displayName;
      string modelName;

      deckLinkDevice.GetDisplayName(out displayName);
      deckLinkDevice.GetModelName(out modelName);

      Run(() => notifications.Text = string.Format("Device disconnected! {0}", FormatDevice(displayName, modelName)));
    }

    private void find_Click(object sender, EventArgs e)
    {
      // Create the COM instance
      IDeckLinkIterator deckLinkIterator = new CDeckLinkIterator();
      if (deckLinkIterator == null)
      {
        MessageBox.Show("Deck link drivers are not installed!", "Error");
        return;
      }
      
      // Get the first DeckLink card
      deckLinkIterator.Next(out _DeckLink);
      if (_DeckLink == null)
      {
        stream.Enabled = false;
        MessageBox.Show("No connected decklink device found", "Error");
        return;
      }

      string displayName;
      string modelName;

      _DeckLink.GetDisplayName(out displayName);
      _DeckLink.GetModelName(out modelName);

      stream.Enabled = true;
      this.deviceName.Text = string.Format("Device chosen: {0}", FormatDevice(displayName, modelName));
    }

    private void stream_Click(object x, EventArgs y)
    {
      // Get the IDeckLinkOutput interface
      var input = (IDeckLinkInput)_DeckLink;
      var output = (IDeckLinkOutput)_DeckLink;
            
      if (_Streaming)
      {
        KillStream();
      }
      else
      {
        IDeckLinkDisplayModeIterator displayIterator;
        input.GetDisplayModeIterator(out displayIterator);

        var supportedModes = new List<IDeckLinkDisplayMode>();
        
        input.SetCallback(this);
        input.SetScreenPreviewCallback(this);

        var flags = _BMDVideoInputFlags.bmdVideoInputFlagDefault | _BMDVideoInputFlags.bmdVideoInputEnableFormatDetection;
        var format = _BMDPixelFormat.bmdFormat8BitYUV;
        var display = _BMDDisplayMode.bmdModeHD1080i50;

        _BMDDisplayModeSupport support;
        IDeckLinkDisplayMode tmp;
        input.DoesSupportVideoMode(display, format, flags, out support, out tmp);

        if (support != _BMDDisplayModeSupport.bmdDisplayModeSupported)
          throw new Exception("display mode not working: " + support);

        _VideoWriter = new BinaryWriter(File.Open("c:/Temp/video.raw", FileMode.OpenOrCreate));

        input.EnableVideoInput(display, format, flags);
        input.EnableAudioInput(_AudioSampleRate, _AudioSampleType, _AudioChannels);
        
        input.StartStreams();

        stream.Text = "Kill";
        _Streaming = true;
      }
    }

    private void KillStream()
    {
      if (!_Streaming) return;

      var input = (IDeckLinkInput)_DeckLink;

      input.StopStreams();
      input.DisableVideoInput();
      input.DisableAudioInput();

      stream.Text = "Stream";
      _Streaming = false;
      _FrameCount = 0;
      _PreviewCount = 0;
      frameCount.Text = string.Empty;
      previewCount.Text = string.Empty;

      if (_VideoWriter != null)
      {
        _VideoWriter.Close();
        _VideoWriter.Dispose();
        _VideoWriter = null;
      }
    }

    /// <summary>
    /// SetCallback
    /// </summary>
    public void VideoInputFormatChanged(_BMDVideoInputFormatChangedEvents notificationEvents, IDeckLinkDisplayMode newDisplayMode, _BMDDetectedVideoInputFormatFlags detectedSignalFlags)
    {
      if (newDisplayMode == null) return;

      Run(() => notifications.Text = string.Format("Video format changed: mode={0}", newDisplayMode.GetDisplayMode()));
      Marshal.ReleaseComObject(newDisplayMode);
    }

    public void VideoInputFrameArrived(IDeckLinkVideoInputFrame videoFrame, IDeckLinkAudioInputPacket audioPacket)
    {
      if (videoFrame == null && audioPacket == null) return;
      try
      {
        Interlocked.Increment(ref _FrameCount);
        Run(() => frameCount.Text = _FrameCount.ToString());

        if (videoFrame != null)
        {
          var rowBytes = videoFrame.GetRowBytes();
          var height = videoFrame.GetHeight();

          IntPtr framePointer;
          videoFrame.GetBytes(out framePointer);

          var frame = new byte[rowBytes * height];
          Marshal.Copy(framePointer, frame, 0, frame.Length);

          // Write it to file
          _VideoWriter.Write(frame);
        }

        /*if (audioPacket != null)
        {
          IntPtr audioPointer;
          audioPacket.GetBytes(out audioPointer);

          var frameCount = audioPacket.GetSampleFrameCount();

          var audio = new byte[frameCount * _AudioChannels * (_AudioSampleDepth / 8)];
          Marshal.Copy(audioPointer, audio, 0, audio.Length);          
        }*/
      }
      finally
      {
        if (videoFrame != null) Marshal.ReleaseComObject(videoFrame);
        if (audioPacket != null) Marshal.ReleaseComObject(audioPacket);
      }
    }

    /// <summary>
    /// SetScreenPreviewCallback
    /// </summary>
    public void DrawFrame(IDeckLinkVideoFrame theFrame)
    {
      if (theFrame == null) return;
      try
      {
        Interlocked.Increment(ref _PreviewCount);
        Run(() => previewCount.Text = _PreviewCount.ToString());

        _GLHelper.SetFrame(theFrame);

        previewBox.MakeCurrent();

        _GLHelper.PaintGL();

        previewBox.SwapBuffers();
        previewBox.Context.MakeCurrent(null);
      }
      finally
      {
        Marshal.ReleaseComObject(theFrame);
      }
    }
    
    private void SetupPreviewBox()
    {
      int w = previewBox.Width;
      int h = previewBox.Height;

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(-1, 1, -1, 1, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
      GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
      
      _GLHelper.InitializeGL();

      GL.ClearColor(Color.SkyBlue); // So we can see the box is actually workign
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      previewBox.SwapBuffers();

      previewBox.Context.MakeCurrent(null);
    }

    // BMD CALLBACKS:

    public void StreamingDeviceArrived(IDeckLink device)
    {
    }

    public void StreamingDeviceRemoved(IDeckLink device)
    {
    }

    public void StreamingDeviceModeChanged(IDeckLink device, _BMDStreamingDeviceMode mode)
    {
    }
  }
}
