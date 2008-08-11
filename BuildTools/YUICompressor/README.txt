==============================================================================
YUI Compressor
==============================================================================

NAME

  YUI Compressor - The Yahoo! JavaScript and CSS Compressor

SYNOPSIS

  Usage: java -jar yuicompressor.jar [options] file
  Options
    -h, --help             Displays this information
    --type <js|css>        Specifies the type of the input file
    --charset <charset>    Read the input file using <charset>
    --line-break <column>  Insert a line break after the specified column number
    -o <file>              Place the output into <file>
    --warn                 [js only] Display possible errors in the code
    --nomunge              [js only] Minify only, do not obfuscate
    --preserve-semi        [js only] Preserve unnecessary semicolons

DESCRIPTION

  The YUI Compressor is a JavaScript compressor which, in addition to removing
  comments and white-spaces, obfuscates local variables using the smallest
  possible variable name. This obfuscation is safe, even when using constructs
  such as 'eval' or 'with' (although the compression is not optimal is those
  cases) Compared to jsmin, the average savings is around 20%.

  The YUI Compressor is also able to safely compress CSS files. The decision
  on which compressor is being used is made on the file extension (js or css)

GLOBAL OPTIONS

  -h, --help
      Prints help on how to use the YUI Compressor

  --line-break
      Some source control tools don't like files containing lines longer than,
      say 8000 characters. The linebreak option is used in that case to split
      long lines after a specific column. It can also be used to make the code
      more readable, easier to debug (especially with the MS Script Debugger)

  --type js|css
      The type of compressor (JavaScript or CSS) is chosen based on the
      extension of the input file name (.js or .css) If the input file name
      does not have an extension, or that extension is neither .js nor .css,
      the type option MUST be specified.

  --charset character-set
      If a supported character set is specified, the YUI Compressor will use it
      to read the input file. Otherwise, it will assume that the platform's
      default character set is being used. The output file is encoded using
      the same character set.

  -o outfile
      Place output in file outfile. If not specified, the YUI Compressor will
      place the output in a file which name is made of the input file name,
      the "-min" suffix and the "js" extension.

JAVASCRIPT ONLY OPTIONS

  --nomunge
      Minify only. Do not obfuscate local symbols.

  --warn
      Prints additional warnings such as duplicate variable declarations,
      missing variable declaration, unrecommended practices, etc.

  --preserve-semi
      Preserve unnecessary semicolons (such as right before a '}') This option
      is useful when compressed code has to be run through JSLint (which is the
      case of YUI for example)

NOTES

  The YUI Compressor requires Java version >= 1.4.

AUTHOR

  The YUI Compressor was written and is maintained by:
      Julien Lecomte <jlecomte@yahoo-inc.com>
  The CSS portion is a port of Isaac Schlueter's cssmin utility.

COPYRIGHT

  Copyright (c) 2007, Yahoo! Inc. All rights reserved.
  Code licensed under the BSD License:
      http://developer.yahoo.net/yui/license.txt
