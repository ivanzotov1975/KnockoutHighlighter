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

    // special keyword Color
    private Color _specialKeywordForeground = Color.OrangeRed;
    private string _specialKeywordForegroundHex = "#FFFF4500";

    // value background Color
    private Color _valueBackground = Color.DarkBlue;
    private string _valueBackgroundHex = "#FF00008B";

    // comment Color
    private Color _commentColor = Color.Green;
    private string _commentColorHex = "#008000";

    [Category("Colors")]
    [DisplayName("Comment Text Color")]
    [Description("Text Color for comments in the 'data-bind' attribute value context.")]
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
      "text, html, css, style, attr, visible, hidden, enable, disable, disabled, value, valueUpdate, checked, hasFocus, selectedOptions, options, optionsText, optionsValue, optionsCaption, optionsAfterRender, foreach, if, ifnot, with, using, template, component, click, event, submit, keypress, keydown, keyup, hover, hasFocus, contextMenu, mouseover, mouseout, mouseenter, mouseleave, mousedown, mouseup, mousemove, dragstart, drag, dragend, drop, allowDrop, checkedValue, textInput, name, id";

    [Category("Keywords")]
    [DisplayName("Keywords")]
    [Description("Comma-separated list of keywords in the 'data-bind' attribute value to highlight (e.g., value, attr, hidden)")]
    public string Keywords
    {
      get => _keywords;
      set => _keywords = value;
    }
    //

    // Special Keywords to highlight with higher priority
    private string _specialKeywords =
      "$data, $parent, $parents, $root, $index, $parentContext, $context, $component, $element, $rawData, $componentTemplateNodes";
    [Category("Keywords")]
    [DisplayName("Special Keywords (binding context)")]
    [Description("Comma-separated list of special keywords in the 'data-bind' attribute value to highlight with higher priority (e.g., $data, $parent). " +
      "KnockoutJS provides several special keywords, known as binding context properties or \"pseudovariables,\" which allow access to data and functions at different levels of your view model hierarchy.")]
    public string SpecialKeywords
    {
      get => _specialKeywords;
      set => _specialKeywords = value;
    }
    //


    // Attribute Name Color
    [Category("Colors")]
    [DisplayName("Attribute 'data-bind' Text Color")]
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
    [Description("Color for brackets inside 'data-bind' attribute value.")]
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

    // Keyword Text Color
    [Category("Colors")]
    [DisplayName("Keyword Text Color")]
    [Description("Text Color used for keywords from the list, like value:, attr:, visible:, etc.")]
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

    // Special Keyword Text Color
    [Category("Colors")]
    [DisplayName("Special Keyword Text Color")]
    [Description("Text Color used for special keywords from the list, like $data, $parent, $root, etc.")]
    public Color SpecialKeywordForeground
    {
      get
      {
        if(ColorTranslator.FromHtml(SpecialKeywordForegroundHex) is Color c)
        {
          return c;
        }
        else
          return _specialKeywordForeground;
      }
      set
      {
        SpecialKeywordForegroundHex = ColorTranslator.ToHtml(value);
        _specialKeywordForeground = value;
      }
    }
    [Browsable(false)]
    public string SpecialKeywordForegroundHex
    {
      get => _specialKeywordForegroundHex;
      set => _specialKeywordForegroundHex = value;
    }

    // Value Background Color
    [Category("Colors")]
    [DisplayName("'data-bind' value Background Color")]
    [Description("Background Color for the 'data-bind' attribute value.")]
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
    [DisplayName("Make Attribute 'data-bind' Bold")]
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