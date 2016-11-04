/**
 *  PathLexeme.js
 *
 *  copyright 2002, 2013 Kevin Lindsey
 */

/*
 * token type enumerations
 */
PathLexeme.UNDEFINED = 0;
PathLexeme.COMMAND  = 1;
PathLexeme.NUMBER   = 2;
PathLexeme.EOD      = 3;

/**
 *  Create a new instance of PathLexeme
 *
 *  @constructor
 *  @param {Number} type
 *  @param {String} text
 */
function PathLexeme(type, text) {
    this.init(type, text);
}

/**
 *  init
 *
 *  @param {Number} type
 *  @param {String} text
 */
PathLexeme.prototype.init = function(type, text) {
    this.type = type;
    this.text = text;
};

/**
 *  typeis
 *
 *  @param {Number} type
 */
PathLexeme.prototype.typeis = function(type) {
    return this.type == type;
}

if (typeof module !== "undefined") {
    module.exports = PathLexeme;
}
