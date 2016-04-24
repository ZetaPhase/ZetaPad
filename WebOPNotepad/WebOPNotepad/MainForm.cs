﻿using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using SharpJS.Dom;
using SharpJS.Dom.Elements;
using SharpJS.Dom.Styles;
using System.IO.WebStorage;
using System;

namespace WebOPNotepad
{
	public class MainForm : WebForm
    {
        Button saveBtn;
        Button loadBtn;
        TextBox saveFile;
        TextBox loadFile;
        TextArea editArea;
        LocalStorageHandle _localStorage;

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
            loadFile = new TextBox()
            {
                Text = "Enter an existing file.",
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
                #endregion
            };
            editArea.Text = _localStorage.GetItem("save_state.txt") ?? "";
        }

        private void OnSave()
        {
            //Save event
            _localStorage.SetItem(saveFile.Text, editArea.Text);
            _localStorage.SetItem("save_state.txt", editArea.Text);
        }
        private void OnLoad()
        {
            //Load event
            editArea.Text = _localStorage.GetItem(loadFile.Text) ?? "";
        }
    }
}