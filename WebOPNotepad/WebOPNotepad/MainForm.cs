using System;
using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using JSIL.Dom;
using JSIL.Dom.Elements;
using JSIL.Dom.Styles;
using System.IO.IsolatedStorage;

namespace WebOPNotepad
{
    /// <summary>
	/// Main WebForm
	/// </summary>
	public class MainForm : WebForm
    {
        Button saveBtn;
        Button openBtn;
        TextArea editArea;

        public MainForm()
        {
            saveBtn = new Button()
            {
                Text = "Save",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(() => OnSave()),
            };
            openBtn = new Button()
            {
                Text = "Open",
                FontStyle = new FontStyle()
                {
                    FontSize = 12,
                },
                Command = new DelegateCommand(() => JSLibrary.Alert("You have clicked the close button"))
            };
            editArea = new TextArea()
            {
                Text = "Cool text area thing",
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
                openBtn,
                new TextBlock()
                {
                    Text = "(c) 2016 The WhatCubes Team",
                    TextAlign = TextAlign.Center
                },
                //Some controls you want aren't there yet.
                //You use a HTMLControl or somehint i forgot.
                #region random htmlcontrol
                new HtmlControl()
                {
                    Elements = new ElementGroup()
                    {
                        new AnchorElement()
                        {
                            HREF = "http://example.com",
                            TextContent = "This is this random link",
                        },
                        new ParagraphElement()
                        {
                            Style = "text-align: center;", //For all your CSS
                            TextContent = "Here's this random paragraph like what",
                            //and so on
                        },
                        //most common elements are there
                        //worst case,
                        //for an element thats not there, use:
                        new Element("video")
                        {

                        }//if you can, don't use the thing with string ^^
                        //SharpJS intelligently does layout based on the element
                        //so use the specific ones like AnchorElement if you can!
                        //good luck!
                        //you can use a UL for the menu  or a bunch of anchors.
                        //for click events:
                        //you need to define a variable
                    }
                },
#endregion
            };
        }

        private void OnSave()
        {
            //Save event
            var storageScope = IsolatedStorageFile.GetUserStoreForAssembly();
            JSConsole.Log(storageScope);
            //This uses streams
            //so ill teach you streamsou

            VirtualFileHelper.WriteAllText(storageScope, "file.txt", editArea.Text);
        }
    }
}