﻿using G1ANT.Language;
using System;

namespace G1ANT.Addon.Flipkart
{
    public class SeleniumCommandArguments : CommandArguments
    {
        [Argument(Required = false, Tooltip = "Phrase to find an element by")]
        public TextStructure Search { get; set; } = new TextStructure(string.Empty);

        [Argument(Tooltip = "Specifies an element selector: 'id', 'class', 'cssselector', 'tag', 'xpath', 'name', 'query', 'jquery'")]
        public TextStructure By { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());

        [Argument(Tooltip = "Phrase to find an iframe by")]
        public TextStructure IFrameSearch { get; set; }

        [Argument(Tooltip = "Specifies an iframe selector: 'id', 'class', 'cssselector', 'tag', 'xpath', 'name', 'query', 'jquery'")]
        public TextStructure IFrameBy { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());
    }
}
