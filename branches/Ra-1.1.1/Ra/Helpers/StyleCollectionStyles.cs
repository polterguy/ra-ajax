using System;
using System.Collections.Generic;
using System.Text;

namespace Ra.Widgets
{
    /**
     * Styles keys for StyleCollection values
     */
    public enum Styles
    {
        /**
         * background: [color] [image - url(x.gif)] [repeat - no-repeat | repeat-x | repeat-y] [scroll | noscroll] [left - unit] [top - unit];
         * Background information for element
         */
        background,
        backgroundAttachment,

        /**
         * background-color: [color]
         * Background color of element
         */
        backgroundColor,

        /**
         * background-image: [image - url(x.gif)]
         * Background image of element
         */
        backgroundImage,
        backgroundPosition,
        backgroundRepeat,

        /**
         * border: [type - solid | dashed | ...] [width - unit] [color]
         * Border settings for elements
         */
        border,
        borderBottom,
        borderBottomColor,
        borderBottomStyle,
        borderBottomWidth,
        borderColor,
        borderLeft,
        borderLeftColor,
        borderLeftStyle,
        borderLeftWidth,
        borderRight,
        borderRightColor,
        borderRightStyle,
        borderRightWidth,
        borderStyle,
        borderTop,
        borderTopColor,
        borderTopStyle,
        borderTopWidth,
        borderWidth,
        clear,

        /**
         * cursor: [cursor type - move | default | ...]
         */
        cursor,

        /**
         * display: [display type - block | inline | none | ...]
         * The way the element will be displayed (or not)
         */
        display,

        /**
         * float: [left | right]
         * The way the element floats in the document
         */
        floating,

        /**
         * position: [absolute | fixed | relative | static]
         * position in flow layout (or not) of element
         */
        position,

        /**
         * visibility: [hidden | visible | ...]
         * visibility of element
         */
        visibility,

        /**
         * height: [unit]
         * height of element
         */
        height,
        lineHeight,
        maxHeight,
        maxWidth,
        minHeight,
        minWidth,

        /**
         * width: [unit]
         * width of element
         */
        width,
        font,
        fontFamily,
        fontSize,
        fontSizeAdjust,
        fontStretch,
        fontStyle,
        fontVariant,
        fontWeight,
        content,
        counterIncrement,
        counterReset,
        quotes,
        listStyle,
        listStyleImage,
        listStylePosition,
        listStyleType,
        markerOffset,
        margin,
        marginBottom,
        marginLeft,
        marginRight,
        marginTop,
        outline,
        outlineColor,
        outlineStyle,
        outlineWidth,
        padding,
        paddingBottom,
        paddingLeft,
        paddingRight,
        paddingTop,
        bottom,
        clip,
        left,
        overflow,
        right,
        top,
        verticalAlign,
        zIndex,
        borderCollapse,
        borderSpacing,
        captionSize,
        emptyCells,
        tableLayout,
        color,
        direction,
        letterSpacing,
        textAlign,
        textDecoration,
        textIndent,
        textShadow,
        textTransform,
        unicodeBidi,
        whiteSpace,
        wordSpacing,
        // We've intentionally added support for opacity to abstract away 
        // the problem of setting the opacity for Elements...
        /**
         * opacity: [0.0 - 1.0]
         * transparency of element. Note this is NOT part of CSS standard but will work in Ra-Ajax
         */
        opacity
    }
}
