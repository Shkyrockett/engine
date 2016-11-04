/**
 *  SampleHandler.js
 *
 *  copyright 2003, 2013 Kevin Lindsey
 */

/**
 *  SampleHandler
 *
 *  @constructor
 */
function SampleHandler() {}

/**
 *  show
 *
 *  @param {String} name
 *  @param {String} params+
 */
SampleHandler.prototype.show = function(name, params) {
    var result = [];
    var args = [];

    for (var i = 0; i < params.length; i++ )
        args[i] = params[i];

    result.push(name);
    result.push("(");
    result.push(args.join(","));
    result.push(")");

    console.log(result.join(""));
};

/**
 *  arcAbs - A
 *
 *  @param {Number} rx
 *  @param {Number} ry
 *  @param {Number} xAxisRotation
 *  @param {Boolean} largeArcFlag
 *  @param {Boolean} sweepFlag
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.arcAbs = function(rx, ry, xAxisRotation, largeArcFlag, sweepFlag, x, y) {
    this.show("arcAbs", arguments);
};

/**
 *  arcRel - a
 *
 *  @param {Number} rx
 *  @param {Number} ry
 *  @param {Number} xAxisRotation
 *  @param {Boolean} largeArcFlag
 *  @param {Boolean} sweepFlag
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.arcRel = function(rx, ry, xAxisRotation, largeArcFlag, sweepFlag, x, y) {
    this.show("arcRel", arguments);
};

/**
 *  curvetoCubicAbs - C
 *
 *  @param {Number} x1
 *  @param {Number} y1
 *  @param {Number} x2
 *  @param {Number} y2
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoCubicAbs = function(x1, y1, x2, y2, x, y) {
    this.show("curvetoCubicAbs", arguments);
};

/**
 *  curvetoCubicRel - c
 *
 *  @param {Number} x1
 *  @param {Number} y1
 *  @param {Number} x2
 *  @param {Number} y2
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoCubicRel = function(x1, y1, x2, y2, x, y) {
    this.show("curvetoCubicRel", arguments);
};

/**
 *  linetoHorizontalAbs - H
 *
 *  @param {Number} x
 */
SampleHandler.prototype.linetoHorizontalAbs = function(x) {
    this.show("linetoHorizontalAbs", arguments);
};

/**
 *  linetoHorizontalRel - h
 *
 *  @param {Number} x
 */
SampleHandler.prototype.linetoHorizontalRel = function(x) {
    this.show("linetoHorizontalRel", arguments);
};

/**
 *  linetoAbs - L
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.linetoAbs = function(x, y) {
    this.show("linetoAbs", arguments);
};

/**
 *  linetoRel - l
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.linetoRel = function(x, y) {
    this.show("linetoRel", arguments);
};

/**
 *  movetoAbs - M
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.movetoAbs = function(x, y) {
    this.show("movetoAbs", arguments);
};

/**
 *  movetoRel - m
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.movetoRel = function(x, y) {
    this.show("movetoRel", arguments);
};

/**
 *  curvetoQuadraticAbs - Q
 *
 *  @param {Number} x1
 *  @param {Number} y1
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoQuadraticAbs = function(x1, y1, x, y) {
    this.show("curvetoQuadraticAbs", arguments);
};

/**
 *  curvetoQuadraticRel - q
 *
 *  @param {Number} x1
 *  @param {Number} y1
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoQuadraticRel = function(x1, y1, x, y) {
    this.show("curvetoQuadraticRel", arguments);
};

/**
 *  curvetoCubicSmoothAbs - S
 *
 *  @param {Number} x2
 *  @param {Number} y2
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoCubicSmoothAbs = function(x2, y2, x, y) {
    this.show("curvetoCubicSmoothAbs", arguments);
};

/**
 *  curvetoCubicSmoothRel - s
 *
 *  @param {Number} x2
 *  @param {Number} y2
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoCubicSmoothRel = function(x2, y2, x, y) {
    this.show("curvetoCubicSmoothRel", arguments);
};

/**
 *  curvetoQuadraticSmoothAbs - T
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoQuadraticSmoothAbs = function(x, y) {
    this.show("curvetoQuadraticSmoothAbs", arguments);
};

/**
 *  curvetoQuadraticSmoothRel - t
 *
 *  @param {Number} x
 *  @param {Number} y
 */
SampleHandler.prototype.curvetoQuadraticSmoothRel = function(x, y) {
    this.show("curvetoQuadraticSmoothRel", arguments);
};

/**
 *  linetoVerticalAbs - V
 *
 *  @param {Number} y
 */
SampleHandler.prototype.linetoVerticalAbs = function(y) {
    this.show("linetoVerticalAbs", arguments);
};

/**
 *  linetoVerticalRel - v
 *
 *  @param {Number} y
 */
SampleHandler.prototype.linetoVerticalRel = function(y) {
    this.show("linetoVerticalRel", arguments);
};

/**
 *  closePath - z or Z
 */
SampleHandler.prototype.closePath = function() {
    this.show("closePath", arguments);
};

if (typeof module !== "undefine") {
    module.exports = SampleHandler;
}
