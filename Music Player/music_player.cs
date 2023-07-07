using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace MediaPlayer
{
    public class MediaPlayerForm : Form
    {
        private OpenFileDialog fileDialog;
        private ListBox playlistBox;
        private Button selectButton;
        private Button playPauseButton;
        private Button stopButton;
        private Button rewindButton;
        private Button fastForwardButton;
        private Button removeButton;
        private Button clearButton;
        private Label playlistLabel;
        private Label volumeLabel;
        private TrackBar volumeTrackBar;
        private Label statusLabel;

        private List<string> playlist;
        private int currentIndex;
        private SoundPlayer currentSoundPlayer;
        private bool isPlaying;
        private float volume;

        public MediaPlayerForm()
        {
            playlist = new List<string>();
            currentIndex = 0;
            isPlaying = false;
            volume = 0.5f;

            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Audio/Video Files|*.mp3;*.wav;*.ogg;*.mp4;*.avi;*.mkv;*.flv;*.mov;*.wmv;*.webm";

            playlistBox = new ListBox();
            playlistBox.SelectionMode = SelectionMode.Single;
            playlistBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#3498db");
            playlistBox.ForeColor = System.Drawing.Color.White;
            playlistBox.SelectedIndexChanged += PlaylistBox_SelectedIndexChanged;

            selectButton = new Button();
            selectButton.Text = "Select File";
            selectButton.Click += SelectButton_Click;

            playPauseButton = new Button();
            playPauseButton.Text = "Play";
            playPauseButton.Click += PlayPauseButton_Click;

            stopButton = new Button();
            stopButton.Text = "Stop";
            stopButton.Click += StopButton_Click;

            rewindButton = new Button();
            rewindButton.Text = "Rewind";
            rewindButton.Click += RewindButton_Click;

            fastForwardButton = new Button();
            fastForwardButton.Text = "Fast Forward";
            fastForwardButton.Click += FastForwardButton_Click;

            removeButton = new Button();
            removeButton.Text = "Remove";
            removeButton.Click += RemoveButton_Click;

            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Click += ClearButton_Click;

            playlistLabel = new Label();
            playlistLabel.Text = "Playlist";

            volumeLabel = new Label();
            volumeLabel.Text = "Volume";

            volumeTrackBar = new TrackBar();
            volumeTrackBar.Minimum = 0;
            volumeTrackBar.Maximum = 100;
            volumeTrackBar.Value = (int)(volume * 100);
            volumeTrackBar.TickFrequency = 10;
            volumeTrackBar.Scroll += VolumeTrackBar_Scroll;

            statusLabel = new Label();
            statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            TableLayoutPanel buttonPanel = new TableLayoutPanel();
            buttonPanel.RowCount = 1;
            buttonPanel.ColumnCount = 5;
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            buttonPanel.Controls.Add(selectButton, 0, 0);
            buttonPanel.Controls.Add(playPauseButton, 1, 0);
            buttonPanel.Controls.Add(stopButton, 2, 0);
            buttonPanel.Controls.Add(rewindButton, 3, 0);
            buttonPanel.Controls.Add(fastForwardButton, 4, 0);

            TableLayoutPanel playlistPanel = new TableLayoutPanel();
            playlistPanel.RowCount = 3;
            playlistPanel.ColumnCount = 2;
            playlistPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            playlistPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            playlistPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            playlistPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            playlistPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            playlistPanel.Controls.Add(playlistLabel, 0, 0);
            playlistPanel.Controls.Add(playlistBox, 0, 1);
            playlistPanel.Controls.Add(removeButton, 0, 2);
            playlistPanel.Controls.Add(clearButton, 1, 2);

            TableLayoutPanel volumePanel = new TableLayoutPanel();
            volumePanel.RowCount = 2;
            volumePanel.ColumnCount = 2;
            volumePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            volumePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            volumePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            volumePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            volumePanel.Controls.Add(volumeLabel, 0, 0);
            volumePanel.Controls.Add(volumeTrackBar, 0, 1);

            TableLayoutPanel statusPanel = new TableLayoutPanel();
            statusPanel.RowCount = 1;
            statusPanel.ColumnCount = 1;
            statusPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            statusPanel.Controls.Add(statusLabel, 0, 0);

            TableLayoutPanel mainPanel = new TableLayoutPanel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.RowCount = 4;
            mainPanel.ColumnCount = 1;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.Controls.Add(buttonPanel, 0, 0);
            mainPanel.Controls.Add(playlistPanel, 0, 1);
            mainPanel.Controls.Add(volumePanel, 0, 2);
            mainPanel.Controls.Add(statusPanel, 0, 3);

            Controls.Add(mainPanel);

            selectButton.Anchor = AnchorStyles.None;
            playPauseButton.Anchor = AnchorStyles.None;
            stopButton.Anchor = AnchorStyles.None;
            rewindButton.Anchor = AnchorStyles.None;
            fastForwardButton.Anchor = AnchorStyles.None;
            removeButton.Anchor = AnchorStyles.None;
            clearButton.Anchor = AnchorStyles.None;

            playlistBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            volumeTrackBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            playlistBox.SelectionMode = SelectionMode.One;
            playlistBox.SelectedIndexChanged += PlaylistBox_SelectedIndexChanged;

            playlistLabel.Anchor = AnchorStyles.Left;
            volumeLabel.Anchor = AnchorStyles.Left;
            statusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            selectButton.Click += SelectButton_Click;
            playPauseButton.Click += PlayPauseButton_Click;
            stopButton.Click += StopButton_Click;
            rewindButton.Click += RewindButton_Click;
            fastForwardButton.Click += FastForwardButton_Click;
            removeButton.Click += RemoveButton_Click;
            clearButton.Click += ClearButton_Click;
            volumeTrackBar.Scroll += VolumeTrackBar_Scroll;

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            CenterToScreen();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                AddToPlaylist(filePath);
            }
        }

        private void AddToPlaylist(string filePath)
        {
            playlist.Add(filePath);
            playlistBox.Items.Add(Path.GetFileName(filePath));
        }

        private void RemoveFromPlaylist(int index)
        {
            playlist.RemoveAt(index);
            playlistBox.Items.RemoveAt(index);
            if (index < currentIndex)
            {
                currentIndex--;
            }
            else if (index == currentIndex)
            {
                StopMusic();
            }
        }

        private void ClearPlaylist()
        {
            playlist.Clear();
            playlistBox.Items.Clear();
            StopMusic();
            currentIndex = 0;
        }

        private void PlayMusic()
        {
            if (playlist.Count == 0)
            {
                MessageBox.Show("No file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = playlist[currentIndex];
            currentSoundPlayer = new SoundPlayer(filePath);
            currentSoundPlayer.LoadCompleted += CurrentSoundPlayer_LoadCompleted;
            currentSoundPlayer.Play();
            isPlaying = true;
            playPauseButton.Text = "Pause";
            statusLabel.Text = "Now playing: " + Path.GetFileName(filePath);
        }

        private void PauseResumeMusic()
        {
            if (isPlaying)
            {
                currentSoundPlayer.Stop();
                isPlaying = false;
                playPauseButton.Text = "Resume";
            }
            else
            {
                currentSoundPlayer.Play();
                isPlaying = true;
                playPauseButton.Text = "Pause";
            }
        }

        private void StopMusic()
        {
            if (currentSoundPlayer != null)
            {
                currentSoundPlayer.Stop();
                currentSoundPlayer.Dispose();
                currentSoundPlayer = null;
                isPlaying = false;
                playPauseButton.Text = "Play";
                statusLabel.Text = "";
                playlistBox.ClearSelected();
            }
        }

        private void RewindMusic()
        {
            if (currentSoundPlayer != null)
            {
                currentSoundPlayer.Position = TimeSpan.Zero;
            }
        }

        private void FastForwardMusic(int seconds)
        {
            if (currentSoundPlayer != null)
            {
                TimeSpan currentPosition = currentSoundPlayer.Position;
                TimeSpan newPosition = currentPosition.Add(TimeSpan.FromSeconds(seconds));
                TimeSpan clipLength = currentSoundPlayer.SoundDuration;
                if (newPosition > clipLength)
                {
                    newPosition = clipLength;
                }
                currentSoundPlayer.Position = newPosition;
            }
        }

        private void SetVolume(float volume)
        {
            if (currentSoundPlayer != null)
            {
                currentSoundPlayer.Volume = volume;
                this.volume = volume;
            }
        }

        private void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PlaylistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = playlistBox.SelectedIndex;
            if (selectedIndex != -1 && selectedIndex != currentIndex)
            {
                currentIndex = selectedIndex;
                StopMusic();
                PlayMusic();
            }
        }

        private void PlayPauseButton_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                PauseResumeMusic();
            }
            else
            {
                PlayMusic();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopMusic();
        }

        private void RewindButton_Click(object sender, EventArgs e)
        {
            RewindMusic();
        }

        private void FastForwardButton_Click(object sender, EventArgs e)
        {
            FastForwardMusic(10);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = playlistBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                RemoveFromPlaylist(selectedIndex);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearPlaylist();
        }

        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            float newVolume = volumeTrackBar.Value / 100f;
            SetVolume(newVolume);
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MediaPlayerForm());
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MediaPlayerForm());
        }
    }
}
