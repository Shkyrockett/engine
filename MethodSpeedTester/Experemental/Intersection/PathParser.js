/**
 *  PathParser.js
 *
 *  copyright 2003, 2013 Kevin Lindsey
 */

var PathLexer = require('./PathLexer'),
    PathLexeme = require('./PathLexeme');

/*
 * class constants
 */
PathParser.PARAMCOUNT = {
    A: 7,
    C: 6,
    H: 1,
    L: 2,
    M: 2,
    Q: 4,
    S: 4,
    T: 2,
    V: 1,
    Z: 0
};
PathParser.METHODNAME = {
    A: "arcAbs",
    a: "arcRel",
    C: "curvetoCubicAbs",
    c: "curvetoCubicRel",
    H: "linetoHorizontalAbs",
    h: "linetoHorizontalRel",
    L: "linetoAbs",
    l: "linetoRel",
    M: "movetoAbs",
    m: "movetoRel",
    Q: "curvetoQuadraticAbs",
    q: "curvetoQuadraticRel",
    S: "curvetoCubicSmoothAbs",
    s: "curvetoCubicSmoothRel",
    T: "curvetoQuadraticSmoothAbs",
    t: "curvetoQuadraticSmoothRel",
    V: "linetoVerticalAbs",
    v: "linetoVerticalRel",
    Z: "closePath",
    z: "closePath"
}

/**
 *  PathParser
 */
function PathParser() {
    this._lexer = new PathLexer();
    this._handler = null;
}

/**
 *  parseData
 *
 *  @param {String} pathData
 *  @exception {Error}
 */
PathParser.prototype.parseData = function(pathData) {
    if ( typeof(pathData) != "string" )
        throw new Error("PathParser.parseData: The first parameter must be a string");

    // init handler
    if ( this._handler != null && this._handler.beginParse != null )
        this._handler.beginParse();

    // pass the pathData to the lexer
    var lexer = this._lexer;
    lexer.setPathData(pathData);

    // set mode to signify new path
    // NOTE: BOP means Beginning of Path
    var mode = "BOP";

    // Process all tokens
    var token = lexer.getNextToken();
    while ( !token.typeis(PathLexeme.EOD) ) {
        var param_count;
        var params = new Array();

        // process current token
        switch ( token.type ) {
            case PathLexeme.COMMAND:
                if ( mode == "BOP" && token.text != "M" && token.text != "m" )
                    throw new Error("PathParser.parseData: a path must begin with a moveto command");

                // Set new parsing mode
                mode = token.text;

                // Get count of numbers that must follow this command
                param_count = PathParser.PARAMCOUNT[token.text.toUpperCase()];

                // Advance past command token
                token = lexer.getNextToken();
                break;

            case PathLexeme.NUMBER:
                // Most commands allow you to keep repeating parameters
                // without specifying the command again.  We just assume
                // that is the case and do nothing since the mode remains
                // the same and param_count is already set
                break;

            default:
                throw new Error("PathParser.parseData: unrecognized token type: " + token.type);
        }

        // Get parameters
        for (var i = 0; i < param_count; i++) {
            switch ( token.type ) {
                case PathLexeme.COMMAND:
                    throw new Error("PathParser.parseData: parameter must be a number: " + token.text);

                case PathLexeme.NUMBER:
                    // convert current parameter to a float and add to
                    // parameter list
                    params[i] = token.text - 0;
                    break;

                default:
                    throw new Errot("PathParser.parseData: unrecognized token type: " + token.type);
            }
            token = lexer.getNextToken();
        }

        // fire handler
        if ( this._handler != null ) {
            var handler = this._handler;
            var method = PathParser.METHODNAME[mode];

            if ( handler[method] != null )
                handler[method].apply(handler, params);
        }

        // Lineto's follow moveto when no command follows moveto params.  Go
        // ahead and set the mode just in case no command follows the moveto
        // command
        if ( mode == "M" ) mode = "L";
        if ( mode == "m" ) mode = "l";
    }
};

/**
 *  setHandler
 *
 *  @param {Object} handler
 */
PathParser.prototype.setHandler = function(handler) {
    //if ( typeof(handler) != "object" )
    //    throw new Error("PathParser.setHandler: first parameter must be an object");

    this._handler = handler;
};

if (typeof module !== "undefined") {
    module.exports = PathParser;
}
