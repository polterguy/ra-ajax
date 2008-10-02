/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    /**
     * Will flash (highlight with color yellow) control's root DOM element
     */
    public class EffectHighlight : Effect
    {
        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectHighlight()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectHighlight(Control control, int milliseconds)
			: base(control, milliseconds)
		{ }

        public override string RenderParalledOnStart()
        {
            return @"
    this._startColor = this.element.getStyle('backgroundColor') || '#ffffff';
    if( this._startColor.toLowerCase() == 'transparent' || 
      (this._startColor.indexOf('rgba(') != -1 && parseInt(this._startColor.split(',')[3], 10) == 0))
      this._startColor = '#fff';
    this._orColor = this.element.getStyle('backgroundColor');
";
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setStyle('backgroundColor',this._orColor);
";
        }

        public override string RenderParalledOnRender()
        {
            return @"
    var color;
    var clr = this._startColor;
    if (clr.slice(0,4) == 'rgb(') {
      var cols = clr.slice(4,clr.length-1).split(',');
      color = {r:parseInt(cols[0], 10), g:parseInt(cols[1], 10), b:parseInt(cols[2], 10)};
    }
    else if (clr.slice(0,5) == 'rgba(') {
      var cols = clr.slice(5,clr.length-1).split(',');
      color = {r:parseInt(cols[0], 10), g:parseInt(cols[1], 10), b:parseInt(cols[2], 10)};
    } else {
      if(clr.slice(0,1) == '#') {
        if(clr.length==4)
          color = {r:parseInt(clr.charAt(1)+clr.charAt(1), 16),g:parseInt(clr.charAt(2)+clr.charAt(2), 16),b:parseInt(clr.charAt(3)+clr.charAt(3), 16)};
        if(clr.length==7) {
          color = {r:parseInt(clr.charAt(1)+clr.charAt(2), 16),g:parseInt(clr.charAt(3)+clr.charAt(4), 16),b:parseInt(clr.charAt(5)+clr.charAt(6), 16)};
        }
      }
    }
    var dR = 255 - color.r;
    var dG = 255 - color.g;
    color.r = parseInt(Math.min(255, ((1.0-pos)*dR) + color.r), 10);
    color.g = parseInt(Math.min(255, ((1.0-pos)*dG) + color.g), 10);
    color.b = parseInt(Math.min(255, pos*color.b), 10);
    var sr, sg, sb;
    sr = (color.r < 16 ? '0' : '') + (color.r ? color.r.toString(16) : '0');
    sg = (color.g < 16 ? '0' : '') + (color.g ? color.g.toString(16) : '0');
    sb = (color.b < 16 ? '0' : '') + (color.b ? color.b.toString(16) : '0');
    this.element.setStyle('backgroundColor','#' + sr + sg + sb);
";
        }
    }
}
