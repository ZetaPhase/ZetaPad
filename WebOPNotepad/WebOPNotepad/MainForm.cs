using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using SharpJS.Dom;
using SharpJS.Dom.Styles;
using System.IO.WebStorage;

namespace WebOPNotepad
{
    public class MainForm : WebForm
    {
        private LocalStorageHandle _localStorage;
        private Button color;
        private Button colorOk;
        private TextBox colorPicker;
        private TextArea editArea;
        private Button loadBtn;
        private TextBox loadFile;
        private Button saveBtn;
        private TextBox saveFile;
        public MainForm()
        {
            _localStorage = new LocalStorageHandle();
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
                Command = new DelegateCommand(() => OnSave()),
            };
            loadBtn = new Button()
            {
                Text = "Load",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(() => OnLoad()),
            };
            saveFile = new TextBox()
            {
                Text = "Enter a name.",
            };
            loadFile = new TextBox
            {
                Text = "Enter an existing file.",
            };
            colorPicker = new TextBox
            {
                Text = "Enter an RGB value",
            };
            colorOk = new Button
            {
                Text = "OK",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(() => OnColorOk())
            };
            color = new Button()
            {
                Text = "Color",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(() => OnColor()),
            };
            Controls = new Layout()
            {
                new TextBlock()
                {
                    Text = "WebOPNotepad",
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
                loadBtn,
                loadFile,
                color,
                colorPicker,
                colorOk,
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
            colorPicker.InternalJQElement.Css("display", "none"); //hide it
            colorOk.InternalJQElement.Css("display", "none");
        }

        private void OnColor()
        {
            //Color click event
            //editArea.Text = "You have clicked the color button";
            JSConsole.Log("OnColor Event fired");
            colorPicker.InternalJQElement.FadeIn();
            colorOk.InternalJQElement.FadeIn();
        }

        private void OnColorOk()
        {
            editArea.InternalJQElement.Css("color", "rgb(0, 255, 255)");
            //color.InternalJQElement.Css(color="blue");
        }

        private void OnLoad()
        {
            //Load event
            editArea.Text = _localStorage.GetItem(loadFile.Text) ?? "";
        }

        private void OnSave()
        {
            //Save event
            _localStorage.SetItem(saveFile.Text, editArea.Text);
            _localStorage.SetItem("save_state.txt", editArea.Text);
        }
    }
}