using Eto.Drawing;
using Eto.Forms;
using System;
using Rhino;
using Rhino.Collections;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Display;
using FrameLineEto.Common.Methods;
using FrameLineEto.Common.Commands;
using System.Data;
using System.Linq;

namespace FrameLineEto.Common.Views
{
    class FrameLineEtoModelessForm : Form
    {
        public FrameLineEtoModelessForm()
        {
            Maximizable = false;
            Minimizable = false;
            Padding = new Padding(5);
            Resizable = false;
            ShowInTaskbar = true;
            Title = GetType().Name;
            WindowStyle = WindowStyle.Default;
            var _Width = 65;
            string fLayerName = "FRAME LINE";

            // Lists storing text boxes for parameters of main line and its modification
            RhinoList<TextBox> main_TextBoxes = new RhinoList<TextBox>();
            RhinoList<TextBox> mod1_TextBoxes = new RhinoList<TextBox>();
            RhinoList<TextBox> mod2_TextBoxes = new RhinoList<TextBox>();
            RhinoList<TextBox> mod3_TextBoxes = new RhinoList<TextBox>();

            #region Frame line
            var main_from_TextBox = new TextBox
            {
                ToolTip = "Type starting frame of frame line.",
                Width = _Width,
            };
            main_TextBoxes.Add(main_from_TextBox);

            var main_to_TextBox = new TextBox
            {
                ToolTip = "Type end frame of frame line.",
                Width = _Width,
            };
            main_TextBoxes.Add(main_to_TextBox);

            var main_spacing_TextBox = new TextBox
            {
                ToolTip = "Type typical spacing of frame line.",
                Width = _Width,
            };
            main_TextBoxes.Add(main_spacing_TextBox);
            #endregion

            #region Frame line modification #1
            var mod1_from_TextBox = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _Width,
            };
            mod1_TextBoxes.Add(mod1_from_TextBox);

            var mod1_to_TextBox = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _Width,
            };
            mod1_TextBoxes.Add(mod1_to_TextBox);

            var mod1_spacing_TextBox = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _Width,
            };
            mod1_TextBoxes.Add(mod1_spacing_TextBox);
            #endregion

            #region Frame line modification #2
            var mod2_from_TextBox = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _Width,
            };
            mod2_TextBoxes.Add(mod2_from_TextBox);

            var mod2_to_TextBox = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _Width,
            };
            mod2_TextBoxes.Add(mod2_to_TextBox);

            var mod2_spacing_TextBox = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _Width,
            };
            mod2_TextBoxes.Add(mod2_spacing_TextBox);
            #endregion

            #region Frame line modification #3
            var mod3_from_TextBox = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _Width,
            };
            mod3_TextBoxes.Add(mod3_from_TextBox);

            var mod3_to_TextBox = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _Width,
            };
            mod3_TextBoxes.Add(mod3_to_TextBox);

            var mod3_spacing_TextBox = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _Width,
            };
            mod3_TextBoxes.Add(mod3_spacing_TextBox);
            #endregion

            #region Buttons
            var clear_main_Button = new Button { Text = "Clear" };
            clear_main_Button.Click += (sender, e) => ClearMainParam(main_TextBoxes);

            var clear_modification_Button = new Button { Text = "Clear" };
            clear_modification_Button.Click += (sender, e) => ClearModParam(mod1_TextBoxes, mod2_TextBoxes, mod3_TextBoxes);

            var create_fLine_Button = new Button { Text = "Create", Width = _Width};
            create_fLine_Button.Click += (sender, e) => CreateFrameLine(main_TextBoxes, mod1_TextBoxes, mod2_TextBoxes, mod3_TextBoxes);

            var close_button = new Button { Text = "Close", Width = _Width };
            close_button.Click += (sender, e) => Close();
            #endregion

            #region Read user data from Layer
            // Setting text boxes texts for values from user data stored in FRAME LINE layer
            if (RhinoDoc.ActiveDoc.Layers.FindName(fLayerName) != null)
            {
                try
                {
                    var userData = RhinoDoc.ActiveDoc.Layers.FindName(fLayerName).UserData.Find(typeof(FrameLineData)) as FrameLineData;
                    if (userData == null)
                    {
                        RhinoApp.WriteLine("userData is null");
                    }
                    else
                    {
                        main_from_TextBox.Text = userData.Main_Params[0].ToString();
                        main_to_TextBox.Text = userData.Main_Params[1].ToString();
                        main_spacing_TextBox.Text = userData.Main_Params[2].ToString();

                        if (userData.Mod1_Params[2] > 0)
                        {
                            mod1_from_TextBox.Text = userData.Mod1_Params[0].ToString();
                            mod1_to_TextBox.Text = userData.Mod1_Params[1].ToString();
                            mod1_spacing_TextBox.Text = userData.Mod1_Params[2].ToString();

                            if (userData.Mod2_Params[2] > 0)
                            {
                                mod2_from_TextBox.Text = userData.Mod2_Params[0].ToString();
                                mod2_to_TextBox.Text = userData.Mod2_Params[1].ToString();
                                mod2_spacing_TextBox.Text = userData.Mod2_Params[2].ToString();

                                if (userData.Mod3_Params[2] > 0)
                                {
                                    mod3_from_TextBox.Text = userData.Mod3_Params[0].ToString();
                                    mod3_to_TextBox.Text = userData.Mod3_Params[1].ToString();
                                    mod3_spacing_TextBox.Text = userData.Mod3_Params[2].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    RhinoApp.WriteLine(ex.ToString());
                }

            }
            #endregion

            #region GroupBox of main frame line
            var main_line_GroupBox = new GroupBox
            {
                Text = "Main parameters of frame line",
                Content = new TableLayout
                {
                    Rows =
                    {
                        new TableRow
                        {
                            Cells =
                            {
                                new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _Width},
                                new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _Width},
                                new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _Width},
                            }
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(main_from_TextBox),
                                new TableCell(main_to_TextBox),
                                new TableCell(main_spacing_TextBox),
                            },
                        },

                        null,
                        new TableRow(null, null, clear_main_Button),
                        null,
                    }
                },
            };
            #endregion

            #region GroupBox - modifications of frame line
            var modification_GroupBox = new GroupBox
            {
                Text = "Local modifications of frame line",
                Content = new TableLayout
                {
                    Rows =
                    {
                        new TableRow
                        {
                            Cells =
                            {
                                new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _Width},
                                new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _Width},
                                new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _Width},
                            }
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(mod1_from_TextBox),
                                new TableCell(mod1_to_TextBox),
                                new TableCell(mod1_spacing_TextBox),
                            },
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(mod2_from_TextBox),
                                new TableCell(mod2_to_TextBox),
                                new TableCell(mod2_spacing_TextBox),
                            },
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(mod3_from_TextBox),
                                new TableCell(mod3_to_TextBox),
                                new TableCell(mod3_spacing_TextBox),
                            },
                        },
                        null,
                        new TableRow(null, null, clear_modification_Button),
                        null,
                    }
                },
            };
            #endregion

            #region bottom buttons
            var bottom_buttons_groupBox = new TableLayout
            {
                Rows =
                {
                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(create_fLine_Button, true),
                            new TableCell(close_button, true),
                        }
                    }
                }
            };
            #endregion

            var layout = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10),
                
                Rows =
                {
                    main_line_GroupBox,
                    null,
                    modification_GroupBox,
                    null,
                    bottom_buttons_groupBox,
                },
            };

            Content = layout;
        }

        public void CreateFrameLine(RhinoList<TextBox> text_List, RhinoList<TextBox> mod1_List, RhinoList<TextBox> mod2_List, RhinoList<TextBox> mod3_List)
        {
            RhinoList<Spacing> spacings = new RhinoList<Spacing>();
            if (!CheckList(text_List))
            {
                return;
            }

            GetParams(text_List, ref spacings);
            if (CheckList(mod1_List)) GetParams(mod1_List, ref spacings);
            if (CheckList(mod2_List)) GetParams(mod2_List, ref spacings);
            if (CheckList(mod3_List)) GetParams(mod3_List, ref spacings);

            CreateFrameLineClass createFrameLine = new CreateFrameLineClass(spacings);
            createFrameLine.AddFrameLineToDoc();

            return;
        }

        void GetParams(RhinoList<TextBox> text_List, ref RhinoList<Spacing> spacingList)
        {
            Spacing spacing = new Spacing(int.Parse(text_List[0].Text),int.Parse(text_List[1].Text),int.Parse(text_List[2].Text));
            spacingList.Add(spacing);
        }

        void ClearMainParam(RhinoList<TextBox> text_List)
        {
            ClearTextBoxes(text_List);
        }

        void ClearModParam(RhinoList<TextBox> text1_List, RhinoList<TextBox> text2_List, RhinoList<TextBox> text3_List)
        {
            ClearTextBoxes(text1_List);
            ClearTextBoxes(text2_List);
            ClearTextBoxes(text3_List);
        }

        void ClearTextBoxes(RhinoList<TextBox> textBoxes)
        {
            foreach (var textbox in textBoxes)
            {
                textbox.Text = "";
            }
        }

        bool CheckList(RhinoList<TextBox> text_List)
        {
            foreach (var text in text_List)
            {
                if(text.Text == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
