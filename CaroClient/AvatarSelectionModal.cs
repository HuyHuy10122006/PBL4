using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CaroClient
{
    public class AvatarSelectionModal : Form
    {
        private readonly string _playerName;
        private Image? _currentAvatar;
        private Image? _selectedAvatar;
        private string? _selectedPresetKey;

        // UI Controls
        private CircularAvatarControl? _previewAvatar;
        private Label? _lblPreviewName;
        private Label? _lblStatusPill;
        private FlowLayoutPanel? _pnlPresets;
        private CyberButton? _btnUpload;
        private CyberButton? _btnConfirm;
        private CyberButton? _btnCancel;
        private Label? _btnClose;

        private readonly List<PresetAvatarItem> _presetControls = new();

        public Image? SelectedAvatarImage => _selectedAvatar;
        public string? SelectedPresetKey => _selectedPresetKey;

        public AvatarSelectionModal(string playerName, Image? currentAvatar)
        {
            _playerName = string.IsNullOrWhiteSpace(playerName) ? "Người chơi" : playerName;
            _currentAvatar = currentAvatar;
            _selectedAvatar = currentAvatar;

            InitializeUI();
        }

        private void InitializeUI()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            Size = new Size(620, 590);
            BackColor = Color.FromArgb(10, 18, 36);
            DoubleBuffered = true;

            // Make form draggable by clicking header
            MouseDown += Form_MouseDown;

            int pad = 24;

            // 1. Title & Subtitle
            Label lblTitle = new Label
            {
                Text = "CHỌN ẢNH ĐẠI DIỆN",
                Font = new Font("Segoe UI", 15f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 220, 255),
                Location = new Point(pad, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(lblTitle);

            Label lblSubtitle = new Label
            {
                Text = "Chọn nhân vật đại diện hoặc tải ảnh yêu thích từ máy tính của bạn",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                ForeColor = Color.FromArgb(140, 165, 200),
                Location = new Point(pad, 48),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(lblSubtitle);

            // Close button (X)
            _btnClose = new Label
            {
                Text = "✕",
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                ForeColor = Color.FromArgb(140, 165, 200),
                Location = new Point(Width - 42, 18),
                Size = new Size(26, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _btnClose.MouseEnter += (s, e) => { if (_btnClose != null) _btnClose.ForeColor = Color.FromArgb(255, 90, 110); };
            _btnClose.MouseLeave += (s, e) => { if (_btnClose != null) _btnClose.ForeColor = Color.FromArgb(140, 165, 200); };
            _btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(_btnClose);

            // 2. Active Preview Card
            RoundedCard cardPreview = new RoundedCard
            {
                Location = new Point(pad, 80),
                Size = new Size(Width - pad * 2, 100),
                FillColor = Color.FromArgb(15, 28, 56),
                BorderColor = Color.FromArgb(0, 194, 255),
                BorderWidth = 1.2f,
                CornerRadius = 12
            };
            Controls.Add(cardPreview);

            _previewAvatar = new CircularAvatarControl
            {
                Location = new Point(16, 10),
                Size = new Size(80, 80),
                AvatarImage = _selectedAvatar,
                BorderRingColor = Color.FromArgb(0, 220, 255),
                ParentBgColor = Color.FromArgb(15, 28, 56),
                IsEditable = false,
                ShowStatusDot = false
            };
            cardPreview.Controls.Add(_previewAvatar);

            _lblPreviewName = new Label
            {
                Text = _playerName,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(110, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardPreview.Controls.Add(_lblPreviewName);

            _lblStatusPill = new Label
            {
                Text = "● Đang xem trước",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 230, 118),
                Location = new Point(112, 52),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardPreview.Controls.Add(_lblStatusPill);

            Label lblPreviewHint = new Label
            {
                Text = "Ảnh đại diện sẽ hiển thị trong sảnh đấu và bảng hồ sơ cá nhân",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.FromArgb(130, 150, 180),
                Location = new Point(cardPreview.Width - 300, 52),
                Size = new Size(290, 36),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };
            cardPreview.Controls.Add(lblPreviewHint);

            // 3. Section Title: "Bộ sưu tập nhân vật"
            Label lblPresetsTitle = new Label
            {
                Text = "BỘ SƯU TẬP CÓ SẴN",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(160, 185, 220),
                Location = new Point(pad, 192),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(lblPresetsTitle);

            // 4. Presets Flow Panel
            _pnlPresets = new FlowLayoutPanel
            {
                Location = new Point(pad, 216),
                Size = new Size(Width - pad * 2, 230),
                BackColor = Color.FromArgb(12, 22, 44),
                AutoScroll = false,
                Padding = new Padding(4),
                BorderStyle = BorderStyle.None
            };
            Controls.Add(_pnlPresets);

            LoadPresetAvatars();

            // 5. Button: Upload custom image from PC
            _btnUpload = new CyberButton
            {
                Text = "Tải ảnh từ máy tính (PNG, JPG, BMP)...",
                Location = new Point(pad, 458),
                Size = new Size(Width - pad * 2, 42),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                IsPrimaryGradient = false,
                BorderLineColor = Color.FromArgb(40, 80, 140),
                NormalColor = Color.FromArgb(18, 32, 64),
                HoverColor = Color.FromArgb(28, 50, 96),
                ForeColor = Color.FromArgb(0, 220, 255),
                CustomTextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            _btnUpload.Click += BtnUpload_Click;
            Controls.Add(_btnUpload);

            // 6. Action Buttons at bottom
            _btnConfirm = new CyberButton
            {
                Text = "Lưu thay đổi",
                Location = new Point(Width - pad - 145, 516),
                Size = new Size(145, 44),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                IsPrimaryGradient = true,
                ForeColor = Color.White,
                CustomTextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            _btnConfirm.Click += BtnConfirm_Click;
            Controls.Add(_btnConfirm);

            _btnCancel = new CyberButton
            {
                Text = "Hủy bỏ",
                Location = new Point(_btnConfirm.Left - 110, 516),
                Size = new Size(100, 44),
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                IsPrimaryGradient = false,
                BorderLineColor = Color.FromArgb(40, 60, 95),
                NormalColor = Color.FromArgb(16, 26, 48),
                HoverColor = Color.FromArgb(26, 40, 72),
                ForeColor = Color.FromArgb(180, 195, 215),
                CustomTextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            _btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(_btnCancel);
        }

        private void LoadPresetAvatars()
        {
            var presets = new (string key, string title, string subtitle, string fileName)[]
            {
                ("huy", "Huy", "Chiến binh Cyber", "avatar_huy.png"),
                ("minhquan", "Minh Quân", "Cyan Headset", "avatar_minhquan.png"),
                ("ducanh", "Đức Anh", "Trí tuệ & Kính mắt", "avatar_ducanh.png"),
                ("baonam", "Bảo Nam", "Streetwear Neon", "avatar_baonam.png"),
                ("proplayer", "Quán quân", "Esports Champion", "avatar_proplayer.png"),
                ("default", "CaroKan", "Mặc định hệ thống", "default_avatar.png")
            };

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string assetsDir = Path.Combine(baseDir, "Assets");
            if (!Directory.Exists(assetsDir)) assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            if (!Directory.Exists(assetsDir)) assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "CaroClient", "Assets");
            if (!Directory.Exists(assetsDir)) assetsDir = @"d:\PBL4\CaroClient\Assets";

            foreach (var preset in presets)
            {
                string path = Path.Combine(assetsDir, preset.fileName);
                Image? img = null;
                if (File.Exists(path))
                {
                    try
                    {
                        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using var bmp = new Bitmap(fs);
                        img = new Bitmap(bmp);
                    }
                    catch { }
                }

                if (img == null) continue;

                var item = new PresetAvatarItem(preset.key, preset.title, preset.subtitle, img);
                item.SelectedChanged += PresetItem_SelectedChanged;
                _presetControls.Add(item);
                _pnlPresets.Controls.Add(item);
            }
        }

        private void PresetItem_SelectedChanged(object? sender, EventArgs e)
        {
            if (sender is PresetAvatarItem selectedItem)
            {
                foreach (var ctrl in _presetControls)
                {
                    if (ctrl != selectedItem) ctrl.IsSelected = false;
                }

                _selectedAvatar = selectedItem.AvatarImage;
                _selectedPresetKey = selectedItem.PresetKey;
                _previewAvatar.AvatarImage = _selectedAvatar;
                _lblStatusPill.Text = $"● Chọn: {selectedItem.PresetTitle}";
                _lblStatusPill.ForeColor = Color.FromArgb(0, 220, 255);
            }
        }

        private void BtnUpload_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện từ máy tính",
                Filter = "Tệp hình ảnh (*.png;*.jpg;*.jpeg;*.bmp;*.webp)|*.png;*.jpg;*.jpeg;*.bmp;*.webp|Tất cả tệp (*.*)|*.*",
                CheckFileExists = true
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var original = new Bitmap(stream);

                    // Crop to square and make a 512x512 high-res avatar
                    int minDim = Math.Min(original.Width, original.Height);
                    int cropX = (original.Width - minDim) / 2;
                    int cropY = (original.Height - minDim) / 2;
                    Rectangle srcRect = new Rectangle(cropX, cropY, minDim, minDim);

                    int targetSize = 512;
                    Bitmap squareBmp = new Bitmap(targetSize, targetSize, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(squareBmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawImage(original, new Rectangle(0, 0, targetSize, targetSize), srcRect, GraphicsUnit.Pixel);
                    }

                    _selectedAvatar = squareBmp;
                    _selectedPresetKey = "custom";

                    // Deselect presets
                    foreach (var ctrl in _presetControls) ctrl.IsSelected = false;

                    _previewAvatar.AvatarImage = _selectedAvatar;
                    _lblStatusPill.Text = $"● Đã tải ảnh: {Path.GetFileName(ofd.FileName)}";
                    _lblStatusPill.ForeColor = Color.FromArgb(0, 230, 118);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở tệp ảnh: {ex.Message}", "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (_selectedAvatar == null)
            {
                MessageBox.Show("Vui lòng chọn một ảnh đại diện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw glowing cyan outer border
            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(new Rectangle(1, 1, Width - 2, Height - 2), 16))
            {
                using (Pen glowPen = new Pen(Color.FromArgb(50, 0, 194, 255), 4f))
                {
                    g.DrawPath(glowPen, path);
                }
                using (Pen borderPen = new Pen(Color.FromArgb(0, 194, 255), 1.5f))
                {
                    g.DrawPath(borderPen, path);
                }
            }
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                Message msg = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                DefWndProc(ref msg);
            }
        }
    }

    /// <summary>
    /// Preset Avatar Card item inside the Selection Gallery
    /// </summary>
    public class PresetAvatarItem : Control
    {
        public string PresetKey { get; }
        public string PresetTitle { get; }
        public string PresetSubtitle { get; }
        public Image AvatarImage { get; }

        private bool _isSelected = false;
        private bool _isHovered = false;

        public event EventHandler? SelectedChanged;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    Invalidate();
                }
            }
        }

        public PresetAvatarItem(string key, string title, string subtitle, Image image)
        {
            PresetKey = key;
            PresetTitle = title;
            PresetSubtitle = subtitle;
            AvatarImage = image;

            Size = new Size(272, 66);
            Margin = new Padding(4);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsSelected = true;
            SelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(2, 2, Width - 4, Height - 4);

            // Background
            Color bgColor = _isSelected
                ? Color.FromArgb(20, 42, 80)
                : (_isHovered ? Color.FromArgb(16, 32, 60) : Color.FromArgb(14, 25, 48));

            Color borderColor = _isSelected
                ? Color.FromArgb(0, 220, 255)
                : (_isHovered ? Color.FromArgb(0, 140, 220) : Color.FromArgb(28, 48, 80));

            using (GraphicsPath path = UITheme.CreateRoundedRectanglePath(rect, 10))
            {
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    g.FillPath(bgBrush, path);
                }
                using (Pen borderPen = new Pen(borderColor, _isSelected ? 2f : 1f))
                {
                    g.DrawPath(borderPen, path);
                }
            }

            // Draw circular thumbnail (50x50)
            int thumbD = 50;
            Rectangle thumbRect = new Rectangle(10, (Height - thumbD) / 2, thumbD, thumbD);

            using (GraphicsPath clipPath = new GraphicsPath())
            {
                clipPath.AddEllipse(thumbRect);
                g.SetClip(clipPath);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(AvatarImage, thumbRect);
                g.ResetClip();
            }

            // Glowing ring on thumbnail
            using (Pen ringPen = new Pen(_isSelected ? Color.FromArgb(0, 220, 255) : Color.FromArgb(40, 70, 110), 1.5f))
            {
                g.DrawEllipse(ringPen, thumbRect);
            }

            // Title & Subtitle
            int textX = thumbRect.Right + 12;
            using (Font titleFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(_isSelected ? Color.White : Color.FromArgb(215, 230, 250)))
            {
                g.DrawString(PresetTitle, titleFont, textBrush, textX, 12);
            }

            using (Font subFont = new Font("Segoe UI", 8.2f, FontStyle.Regular))
            using (SolidBrush subBrush = new SolidBrush(Color.FromArgb(135, 160, 195)))
            {
                g.DrawString(PresetSubtitle, subFont, subBrush, textX, 35);
            }

            // Checkmark badge when selected
            if (_isSelected)
            {
                int badgeD = 18;
                int badgeX = Width - badgeD - 10;
                int badgeY = 10;
                using (SolidBrush checkBg = new SolidBrush(Color.FromArgb(0, 230, 118)))
                {
                    g.FillEllipse(checkBg, badgeX, badgeY, badgeD, badgeD);
                }
                using (Font checkFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                using (SolidBrush whiteBrush = new SolidBrush(Color.Black))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("✓", checkFont, whiteBrush, new RectangleF(badgeX, badgeY, badgeD, badgeD), sf);
                }
            }
        }
    }
}
