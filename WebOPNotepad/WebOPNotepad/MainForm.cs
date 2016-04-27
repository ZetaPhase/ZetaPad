using System;
using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using SharpJS.Dom;
using SharpJS.Dom.Styles;
using SharpJS.System.IO.WebStorage;
using SharpJS.JSLibraries;

namespace ZetaPad
{
    public class MainForm : WebForm
    {
        private LocalStorageHandle _localStorage;
        private Button color;
        private Button colorOk;
        private TextBox red;
        private TextBox green;
        private TextBox blue;
        private TextArea editArea;
        private TextArea storedFiles;
        private Button refreshStoredFiles;
        private TextBox storedFilesLabel;
        private Button loadBtn;
        private TextBox loadFile;
        private Button saveBtn;
        private TextBox saveFile;
        private TextBox removeFile;
        private Button clear;
        private Button remove;
        private Button saveOk;
        private Button loadOk;
        private Button removeOk;
		private FileUploadButton uploader;
		private Button uploadFileBtn;
		private Button downloadFileBtn;
        
        public MainForm()
        {
            _localStorage = new LocalStorageHandle();
            uploader = new FileUploadButton
            {
				Command = new ParameterizedCommand(OnFileUploaded),
				UploadType = FileUploadType.TextFile,
				Visible = false,
			};
            uploadFileBtn = new Button
            {
				Text = "Upload from Computer",
				Command = new DelegateCommand(UploadIt),
			};
            downloadFileBtn = new Button
            {
				Text = "Download to Computer",
				Command = new DelegateCommand(DownloadIt),
			};
            storedFilesLabel = new TextBox
            {
                PlaceholderText = "Saved Files"
            };
            refreshStoredFiles = new Button
            {
                Text = "Refresh",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(ModifyStoredFiles),
            };
            storedFiles = new TextArea
            {
                Text = "",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Columns = 7,
            };
            saveOk = new Button
            {
                Text = "OK",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
                Command = new DelegateCommand(() => OnSaveOk())
            };
            loadOk = new Button
            {
                Text = "OK",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
                Command = new DelegateCommand(OnLoadOk)
            };
            removeOk = new Button
            {
                Text = "OK",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
                Command = new DelegateCommand(OnRemoveOk)
            };
            clear = new Button
            #region code
            {
                Text = "Clear All Files",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
                Command = new DelegateCommand(OnClear)
            };
            #endregion
            clear.InternalJQElement.Css("width", "175px");
            remove = new Button
            {
                Text = "Remove",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
                Command = new DelegateCommand(OnRemove)
            };
            removeFile = new TextBox
            {
                Text = "",
                FontStyle = new FontStyle()
                {
                    FontSize = 12
                },
            };
            editArea = new TextArea()
            {
                Text = "",
                Rows = 12,
            };
            saveBtn = new Button()
            {
                Text = "Save",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(OnSave),
            };
            loadBtn = new Button()
            {
                Text = "Load",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(OnLoad),
            };
            saveFile = new TextBox()
            {
                PlaceholderText = "File name.",
            };
            loadFile = new TextBox
            {
                PlaceholderText = "Existing file name.",
            };
            red = new TextBox
            {
                PlaceholderText = "R",
            };
            red.InternalJQElement.Css("width", "50px");
            green = new TextBox
            {
                PlaceholderText = "G"
            };
            green.InternalJQElement.Css("width", "50px");
            blue = new TextBox
            {
                PlaceholderText = "B"
            };
            blue.InternalJQElement.Css("width", "50px");
            colorOk = new Button
            {
                Text = "OK",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(OnColorOk)
            };
            color = new Button()
            {
                Text = "Color",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(OnColor),
            };
            storedFilesLabel.InternalJQElement.Css("content", @"\a"); //the @ means automaticaly escape backslashes
            storedFilesLabel.InternalJQElement.Css("white-space", "pre");
            storedFilesLabel.InternalJQElement.Css("display", "block");
            Controls = new Layout()
            {
                new TextBlock()
                {
                    Text = "ZetaPad",
                    TextAlign = TextAlign.Center,
                    FontStyle = new FontStyle()
                    {
                        FontSize = 24,
                        FontWeight = FontWeight.Bold,
                    }
                },
                editArea,
                saveBtn,
                saveFile,
                saveOk,
                loadBtn,
                loadFile,
                loadOk,
                remove,
                removeFile,
                removeOk,
                color,
                red,
                green,
                blue,
                colorOk,
                clear,
                storedFilesLabel,
                storedFiles,
                refreshStoredFiles,
                downloadFileBtn,
                uploadFileBtn,
                new TextBlock()
                {
                    Text = "(c) 2016 The WhatCubes Team",
                    TextAlign = TextAlign.Center
                },

                #region random htmlcontrol

                new HtmlControl()
                {
                    //Elements = new ElementGroup()
                    //{
                    //    new AnchorElement()
                    //    {
                    //        HREF = "http://example.com",
                    //        TextContent = "This is this random link",
                    //    },
                    //    new ParagraphElement()
                    //    {
                    //        Style = "text-align: center;",
                    //        TextContent = "Here's this random paragraph like what",
                    //    },
                    //    new Element("video")
                    //    {
                    //    }
                    //}
                },

                #endregion random htmlcontrol
            };
            
            editArea.Text = _localStorage.GetItem("save_state.txt") ?? "";
            //THIS below
            red.InternalJQElement.Css("display", "none"); //hide it
            green.InternalJQElement.Css("display", "none");
            blue.InternalJQElement.Css("display", "none");
            colorOk.InternalJQElement.Css("display", "none");
            saveFile.InternalJQElement.Css("display", "none");
            loadFile.InternalJQElement.Css("display", "none");
            removeFile.InternalJQElement.Css("display", "none");
            saveOk.InternalJQElement.Css("display", "none");
            loadOk.InternalJQElement.Css("display", "none");
            removeOk.InternalJQElement.Css("display", "none");
        }

        private void OnColor()
        {
            //Color click event
            //editArea.Text = "You have clicked the color button";
            red.InternalJQElement.FadeIn();
            green.InternalJQElement.FadeIn();
            blue.InternalJQElement.FadeIn();
            colorOk.InternalJQElement.FadeIn();
        }
        private void ModifyStoredFiles() {
            for (var i = 0; i < _localStorage.Length; i+=1)
            {
                if (storedFiles.Text != "save_state.txt")
                {
                    storedFiles.Text += _localStorage.Key(i) + '\n';
                }
            }
        }
        private void OnColorOk()
        {
            editArea.InternalJQElement.Css("color", String.Format("rgb({0}, {1}, {2})", red.Text, green.Text, blue.Text));
            red.InternalJQElement.FadeOut();
            green.InternalJQElement.FadeOut();
            blue.InternalJQElement.FadeOut();
            colorOk.InternalJQElement.FadeOut();
        }

        private void OnClear()
        {
            //Clear browser storage event
            _localStorage.Clear();
        }

        private void OnSave()
        {
            //Save event
            saveFile.InternalJQElement.FadeIn();
            saveOk.InternalJQElement.FadeIn();
        }
        private void OnSaveOk()
        {
            _localStorage.SetItem(saveFile.Text, editArea.Text);
            _localStorage.SetItem("save_state.txt", editArea.Text);
            saveFile.InternalJQElement.FadeOut();
            saveOk.InternalJQElement.FadeOut();
        }
        private void OnLoad()
        {
            //Load event
            loadFile.InternalJQElement.FadeIn();
            loadOk.InternalJQElement.FadeIn();
        }
        private void OnLoadOk()
        {
            editArea.Text = _localStorage.GetItem(loadFile.Text) ?? "";
            loadFile.InternalJQElement.FadeOut();
            loadOk.InternalJQElement.FadeOut();
        }
        private void OnRemove()
        {
            //remove event
            removeFile.InternalJQElement.FadeIn();
            removeOk.InternalJQElement.FadeIn();
        }
        private void OnRemoveOk()
        {
            _localStorage.RemoveItem(removeFile.Text);
            removeFile.InternalJQElement.FadeOut();
            removeOk.InternalJQElement.FadeOut();
        }
        private void UploadIt()
        {
			uploader.SimulateClick();
        }
        
        private void OnFileUploaded(ICommandParameter parameter)
        {
        	var args = parameter.Parameter as FileUploadEventArgs;
			editArea.Text = args.PlainTextContent;
        }
        private void DownloadIt()
        {
        	FileDownloader.SaveTextToFile(string.IsNullOrWhiteSpace(editArea.Text) ? "myfile.txt" : editArea.Text, saveFile.Text);
        }
    }
}