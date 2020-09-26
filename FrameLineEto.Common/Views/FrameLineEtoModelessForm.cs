using Eto.Drawing;
using Eto.Forms;
using System;
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
            var _width = 65;
            RhinoList<TextBox> main_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod1_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod2_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod3_texts = new RhinoList<TextBox>();

            #region Frame line
            var fromTextBox = new TextBox
            {
                ToolTip = "Type starting frame of frame line.",
                Width = _width,
            };
            main_texts.Add(fromTextBox);
            var toTextBox = new TextBox
            {
                ToolTip = "Type end frame of frame line.",
                Width = _width,
            };
            main_texts.Add(toTextBox);
            var spacingTextBox = new TextBox
            {
                ToolTip = "Type typical spacing of frame line.",
                Width = _width,
            };
            main_texts.Add(spacingTextBox);
            #endregion

            #region Frame line modification #1
            var fromTextBox_1 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod1_texts.Add(fromTextBox_1);
            var toTextBox_1 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod1_texts.Add(toTextBox_1);
            var spacingTextBox_1 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod1_texts.Add(spacingTextBox_1);
            #endregion

            #region Frame line modification #2
            var fromTextBox_2 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod2_texts.Add(fromTextBox_2);
            var toTextBox_2 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod2_texts.Add(toTextBox_2);
            var spacingTextBox_2 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod2_texts.Add(spacingTextBox_2);
            #endregion

            #region Frame line modification #3
            var fromTextBox_3 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod3_texts.Add(fromTextBox_2);
            var toTextBox_3 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod3_texts.Add(toTextBox_2);
            var spacingTextBox_3 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod3_texts.Add(spacingTextBox_3);
            #endregion

            #region Buttons
            var clear_button = new Button { Text = "Clear" };
            clear_button.Click += (sender, e) => ClearMainParam(main_texts);
            var clear_mod_button = new Button { Text = "Clear" };
            clear_mod_button.Click += (sender, e) => ClearModParam(mod1_texts, mod2_texts, mod3_texts);

            var hello_button = new Button { Text = "Create", Width = _width};
            hello_button.Click += (sender, e) => CreateFrameLine(main_texts, mod1_texts, mod2_texts, mod3_texts);

            var close_button = new Button { Text = "Close", Width = _width };
            close_button.Click += (sender, e) => Close();
            #endregion

            #region GroupBox of main frame line
            var main_groupBox = new GroupBox
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
                                new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _width},
                                new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _width},
                                new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _width},
                            }
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(fromTextBox),
                                new TableCell(toTextBox),
                                new TableCell(spacingTextBox),
                            },
                        },

                        null,
                        new TableRow(null, null, clear_button),
                        null,
                    }
                },
            };
            #endregion

            #region GroupBox - modifications of frame line
            var modification_groupBox = new GroupBox
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
                                new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _width},
                                new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _width},
                                new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _width},
                            }
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(fromTextBox_1),
                                new TableCell(toTextBox_1),
                                new TableCell(spacingTextBox_1),
                            },
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(fromTextBox_2),
                                new TableCell(toTextBox_2),
                                new TableCell(spacingTextBox_2),
                            },
                        },
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(fromTextBox_3),
                                new TableCell(toTextBox_3),
                                new TableCell(spacingTextBox_3),
                            },
                        },
                        null,
                        new TableRow(null, null, clear_mod_button),
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
                            new TableCell(hello_button, true),
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
                    main_groupBox,
                    null,
                    modification_groupBox,
                    null,
                    bottom_buttons_groupBox,
                },
            };

            Content = layout;
        }

        public void CreateFrameLine(RhinoList<TextBox> texts, RhinoList<TextBox> texts_mod1, RhinoList<TextBox> texts_mod2, RhinoList<TextBox> texts_mod3)
        {
            RhinoList<Spacing> spacings = new RhinoList<Spacing>();
            if (!CheckList(texts))
            {
                return;
            }

            GetParams(texts, ref spacings);
            if (CheckList(texts_mod1)) GetParams(texts_mod1, ref spacings);
            if (CheckList(texts_mod2)) GetParams(texts_mod2, ref spacings);
            if (CheckList(texts_mod3)) GetParams(texts_mod3, ref spacings);

            CreateFrameLineClass createFrameLine = new CreateFrameLineClass(spacings);
            createFrameLine.AddFrameLineToDoc();

            return;
        }

        void GetParams(RhinoList<TextBox> texts, ref RhinoList<Spacing> spacingList)
        {
            Spacing spacing = new Spacing(int.Parse(texts[0].Text),int.Parse(texts[1].Text),int.Parse(texts[2].Text));
            spacingList.Add(spacing);
        }

        void ClearMainParam(RhinoList<TextBox> texts)
        {
            ClearTextBoxes(texts);
        }

        void ClearModParam(RhinoList<TextBox> texts1, RhinoList<TextBox> texts2, RhinoList<TextBox> texts3)
        {
            ClearTextBoxes(texts1);
            ClearTextBoxes(texts2);
            ClearTextBoxes(texts3);
        }

        void ClearTextBoxes(RhinoList<TextBox> textBoxes)
        {
            foreach (var textbox in textBoxes)
            {
                textbox.Text = "";
            }
        }

        bool CheckList(RhinoList<TextBox> texts)
        {
            foreach (var text in texts)
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
