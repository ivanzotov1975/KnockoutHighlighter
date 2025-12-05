using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace KnockoutHighlighter.DataBind
{
  [Serializable]
  public class OptionsPage : DialogPage
  {
    // attribute Name Color
    private Color _attributeNameColor = Color.Gold;
    private string _attributeNameColorHex = "#FFFFD700";

    private bool _boldAttributeName = true;
    private bool _italicKeywords = true;

    // brackets Color
    private Color _bracketColor = Color.SteelBlue;
    private string _bracketColorHex = "#FF4682B4";

    // keyword Color
    private Color _keywordForeground = Color.MediumVioletRed;
    private string _keywordForegroundHex = "#FFC71585";

    // value background Color
    private Color _valueBackground = Color.DarkBlue;
    private string _valueBackgroundHex = "#FF00008B";

    // comment Color
    private Color _commentColor = Color.Green;  
    private string _commentColorHex = "#008000";

    [Category("Colors")]
    [DisplayName("Comment Color")]
    [Description("Color for comments in the data-bind context.")]
    public Color CommentColor
    {
      get
      {
        if(ColorTranslator.FromHtml(CommentColorHex) is Color c)
        {
          return c;
        }
        else
          return _commentColor;
      }
      set
      {
        CommentColorHex = ColorTranslator.ToHtml(value);
        _commentColor = value;
      }
    }

    [Browsable(false)]
    public string CommentColorHex
    {
      get => _commentColorHex;
      set => _commentColorHex = value;
    }

    // Keywords to highlight
    private string _keywords =
      "text, html, css, style, attr, visible, hidden, enable, disable, value, valueUpdate, checked, hasFocus, selectedOptions, options, optionsText, optionsValue, optionsCaption, optionsAfterRender, foreach, if, ifnot, with, using, template, component, click, event, submit, keypress, keydown, keyup, hover, hasFocus, contextMenu, mouseover, mouseout, mouseenter, mouseleave, mousedown, mouseup, mousemove, dragstart, drag, dragend, drop, allowDrop, checkedValue";

    [Category("Keywords")]
    [DisplayName("Highlight Keywords")]
    [Description("Comma-separated list of keywords to highlight (e.g., value, attr, hidden)")]
    public string Keywords
    {
      get => _keywords;
      set => _keywords = value;
    }

    // Attribute Name Color
    [Category("Colors")]
    [DisplayName("Attribute Name Color")]
    [Description("Color for the 'data-bind' attribute name.")]
    public Color AttributeNameColor
    {
      get
      {
        if(ColorTranslator.FromHtml(AttributeNameColorHex) is Color c)
        {
          return c;
        }
        else
          return _attributeNameColor;
      }
      set
      {
        AttributeNameColorHex = ColorTranslator.ToHtml(value);
        _attributeNameColor = value;
      }
    }

    [Browsable(false)]
    public string AttributeNameColorHex
    {
      get => _attributeNameColorHex;
      set => _attributeNameColorHex = value;
    }
    //

    // Bracket Color
    [Category("Colors")]
    [DisplayName("Bracket Color")]
    [Description("Color for brackets inside data-bind values.")]
    public Color BracketColor
    {
      get
      {
        if(ColorTranslator.FromHtml(BracketColorHex) is Color c)
        {
          return c;
        }
        else
          return _bracketColor;
      }
      set
      {
        BracketColorHex = ColorTranslator.ToHtml(value);
        _bracketColor = value;
      }
    }

    [Browsable(false)]
    public string BracketColorHex
    {
      get => _bracketColorHex;
      set => _bracketColorHex = value;
    }
    //

    // Keyword Foreground Color
    [Category("Colors")]
    [DisplayName("Keyword Foreground")]
    [Description("Color used for keywords like value:, attr:, etc.")]
    public Color KeywordForeground
    {
      get
      {
        if(ColorTranslator.FromHtml(KeywordForegroundHex) is Color c)
        {
          return c;
        }
        else
          return _keywordForeground;
      }
      set
      {
        KeywordForegroundHex = ColorTranslator.ToHtml(value);
        _keywordForeground = value;
      }
    }

    [Browsable(false)]
    public string KeywordForegroundHex
    {
      get => _keywordForegroundHex;
      set => _keywordForegroundHex = value;
    }
    //

    // Value Background Color
    [Category("Colors")]
    [DisplayName("Value Background")]
    [Description("Background color for the data-bind value string.")]
    public Color ValueBackground
    {
      get
      {
        if(ColorTranslator.FromHtml(ValueBackgroundHex) is Color c)
        {
          return c;
        }
        else
          return _valueBackground;
      }
      set
      {
        ValueBackgroundHex = ColorTranslator.ToHtml(value);
        _valueBackground = value;
      }
    }

    [Browsable(false)]
    public string ValueBackgroundHex
    {
      get => _valueBackgroundHex;
      set => _valueBackgroundHex = value;
    }
    //

    [Category("Style")]
    [DisplayName("Make Keywords Italic")]
    public bool ItalicKeywords
    {
      get => _italicKeywords;
      set => _italicKeywords = value;
    }

    [Category("Style")]
    [DisplayName("Make Attribute Name Bold")]
    public bool BoldAttributeName
    {
      get => _boldAttributeName;
      set => _boldAttributeName = value;
    }


    public event PropertyChangedEventHandler PropertyChanged;

    // when press OK on property dialog
    protected override void OnApply(PageApplyEventArgs e)
    {
      base.OnApply(e);

      // Apply to all open views
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }


    // event occur when property dialog is going to be open
    //protected override void OnActivate(CancelEventArgs e)
    //{
    //  base.OnActivate(e);
    //  //TextColor = ColorTranslator.FromHtml(textColorHex);
    //}


    //private void ResetColors()
    //{
    //  _attributeNameColor = Color.Gold;
    //  _bracketColor = Color.SteelBlue;
    //  _keywordForeground = Color.MediumVioletRed;
    //  _valueBackground = Color.DarkBlue;
    //  _textColor = Color.Red;
    //}
    //URG: to make option to reset color and to set default for night and day themes

  }

  internal static class ViewTracker
  {
    public static readonly List<IWpfTextView> ActiveViews = new List<IWpfTextView>();
  }

}