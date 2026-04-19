using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using ScreenRecorderLib;
using NAudio.Wave;
using Microsoft.Win32;
using System.Windows.Controls;

namespace GoldRecordDX
{
    public partial class MainWindow : Window
    {
        private Recorder? _recorder;
        private IWavePlayer? _silentPlayer;
        private bool _isRecording = false;
        private bool _isPaused = false;
        private DispatcherTimer? _displayTimer;
        private Stopwatch? _stopwatch;
        private List<AudioDevice>? _audioDevices;
        private string? _selectedFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
            LoadAudioDevices();
        }

        private void StartRecording()
        {
            try
            {
                string? micId = (_audioDevices != null && ComboAudio.SelectedIndex >= 0) ? _audioDevices[ComboAudio.SelectedIndex].DeviceName : null;
                string finalPath = _selectedFilePath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), $"GOLD_{DateTime.Now:yyyyMMdd_HHmm}.mp4");

                // Lógica Direta: Lê o que está escrito no Dropdown
                // HQ = 6Mbps | ECO = 2Mbps
                string quality = ((ComboBoxItem)ComboQuality.SelectedItem).Content.ToString();
                int bitrateValue = (quality == "ECO") ? 2000000 : 6000000;

                var opt = new RecorderOptions
                {
                    VideoEncoderOptions = new VideoEncoderOptions
                    {
                        Bitrate = bitrateValue,
                        Framerate = 30,
                        IsHardwareEncodingEnabled = true
                    },
                    AudioOptions = new AudioOptions
                    {
                        IsAudioEnabled = true,
                        IsOutputDeviceEnabled = true,
                        IsInputDeviceEnabled = (micId != null),
                        AudioInputDevice = micId
                    }
                };

                _recorder = Recorder.CreateRecorder(opt);
                _recorder.Record(finalPath);

                StartSilentEngine();
                _stopwatch?.Restart();
                _displayTimer?.Start();

                _isRecording = true;
                BtnRecord.Content = "PARAR";
                BtnPause.IsEnabled = true;
                ComboAudio.IsEnabled = false;
                ComboQuality.IsEnabled = false; // Trava a escolha durante a gravação
            }
            catch (Exception ex) { MessageBox.Show("Erro ao iniciar: " + ex.Message); }
        }

        private void StopRecording()
        {
            _displayTimer?.Stop();
            _stopwatch?.Stop();
            _silentPlayer?.Stop();
            _recorder?.Stop();
            _recorder?.Dispose();
            _isRecording = false;
            _isPaused = false;
            _selectedFilePath = null;
            BtnRecord.Content = "GRAVAR";
            BtnPause.Content = "II";
            BtnPause.IsEnabled = false;
            ComboAudio.IsEnabled = true;
            ComboQuality.IsEnabled = true;
            TxtTimer.Text = "00:00:00";
        }

        // --- MÉTODOS AUXILIARES ---
        private void SetupTimer()
        {
            _stopwatch = new Stopwatch();
            _displayTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _displayTimer.Tick += (s, e) => { if (_stopwatch.IsRunning) TxtTimer.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss"); };
        }

        private void LoadAudioDevices()
        {
            try
            {
                _audioDevices = Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices);
                ComboAudio.Items.Clear();
                if (_audioDevices != null)
                {
                    foreach (var dev in _audioDevices) ComboAudio.Items.Add(dev.FriendlyName);
                    if (ComboAudio.Items.Count > 0) ComboAudio.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void BtnFolder_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog { Filter = "Vídeo MP4|*.mp4", Title = "Salvar em...", FileName = $"GOLD_{DateTime.Now:yyyyMMdd_HHmm}" };
            if (s.ShowDialog() == true) { _selectedFilePath = s.FileName; MessageBox.Show("Destino definido!"); }
        }

        private void BtnRecord_Click(object sender, RoutedEventArgs e) { if (!_isRecording) StartRecording(); else StopRecording(); }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (!_isPaused) { _recorder?.Pause(); _stopwatch?.Stop(); BtnPause.Content = "▶"; _isPaused = true; }
            else { _recorder?.Resume(); _stopwatch?.Start(); BtnPause.Content = "II"; _isPaused = false; }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { DragMove(); if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal; }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void BtnClose_Click(object sender, RoutedEventArgs e) { _silentPlayer?.Dispose(); _recorder?.Dispose(); Application.Current.Shutdown(); }

        private void StartSilentEngine()
        {
            try
            {
                var sineWave = new NAudio.Wave.SampleProviders.SignalGenerator(44100, 1) { Type = NAudio.Wave.SampleProviders.SignalGeneratorType.Sin, Frequency = 1, Gain = 0.05 };
                _silentPlayer = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 200);
                _silentPlayer.Init(sineWave);
                _silentPlayer.Play();
            }
            catch { }
        }
    }
}