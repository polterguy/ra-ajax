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
    public class EffectHighlight : Effect
    {
        public EffectHighlight(Control control, decimal seconds)
			: base(control, seconds)
		{ }

		// For chained effects
        public EffectHighlight()
			: base(null, 0.0M)
		{ }
		
        public override string RenderChainedOnStart()
        {
            return @"
    this._startColor = this.element.style.backgroundColor;
";
        }

        public override string RenderChainedOnFinished()
        {
            return @"
    this.element.style.backgroundColor = this._startColor;
";
        }

        public override string RenderChainedOnRender()
        {
            return @"
    var color;
    var clr = this._startColor;
    if (clr.slice(0,4) == 'rgb(') {
      var cols = clr.slice(4,clr.length-1).split(',');
      color = {r:parseInt(cols[0], 10), g:parseInt(cols[1], 10), b:parseInt(cols[2], 10)};
    } else {
      if(clr.slice(0,1) == '#') {
        if(this.length==4)
          color = {r:parseInt(clr.charAt(1)+clr.charAt(1), 16),g:parseInt(clr.charAt(2)+clr.charAt(2), 16),b:parseInt(clr.charAt(3)+clr.charAt(3), 16)};
        if(this.length==7)
          color = {r:parseInt(clr.charAt(1)+clr.charAt(2), 16),g:parseInt(clr.charAt(3)+clr.charAt(4), 16),b:parseInt(clr.charAt(5)+clr.charAt(6), 16)};
      }
    }
    var dR = 255 - color.r;
    var dG = 255 - color.g;
    color.r = parseInt(Math.min(255, ((1.0-pos)*dR) + color.r), 10);
    color.g = parseInt(Math.min(255, ((1.0-pos)*dG) + color.g), 10);
    color.b = parseInt(Math.min(255, pos*color.b), 10);
    this.element.style.backgroundColor = '#' + color.r.toString(16) + color.g.toString(16) + color.b.toString(16);
";
        }
    }
}
