﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHP.Syntax
{
    #region Language Features Enum

    /// <summary>
    /// PHP language features supported by Phalanger.
    /// </summary>
    [Flags]
    public enum LanguageFeatures
    {
        /// <summary>
        /// Basic features - always present.
        /// </summary>
        Basic = 0,

        /// <summary>
        /// Allows using short open tags in the script.
        /// </summary>
        ShortOpenTags = 1
    }

    #endregion
}