/*Regenerates the strong name verification scripts based on the list of
assemblies stored in this file.

The generated files should be checked in.
*/

var fs = require('fs');
var util = require('util');

var assemblies = [
    'Microsoft.NodejsUwp',
    'NodejsUwp.Tests',
    'TestUtilities.NodejsUwp',
    'TestUtilities',
    'TestUtilities.UI'
].sort();

var files = [
    [ "EnableSkipVerification.reg", true, true],
    [ "EnableSkipVerificationX86.reg", true, false],
    [ "DisableSkipVerification.reg", false, true],
    [ "DisableSkipVerificationX86.reg", false, false],
];

function skipVerification(enable, includeWow) {
    var text = 'Windows Registry Editor Version 5.00\r\n\r\n';
    var length = assemblies.length;

    for (var i = 0; i < length; i++) {
        var name = assemblies[i];
        text += util.format('[%sHKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\StrongName\\Verification\\%s,B03F5F7F11D50A3A]\r\n', enable ? '' : '-', name);
    }

    if (includeWow) {
        for (var i = 0; i < length; i++) {
            var name = assemblies[i];
            text += util.format('[%sHKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\StrongName\\Verification\\%s,B03F5F7F11D50A3A]\r\n', enable ? '' : '-', name);
        }
    }

    return text;
}

var length = files.length;
for (var i = 0; i < length; i++) {
    (function(name, enable, includeWow){
        fs.writeFile(name, skipVerification(enable, includeWow), function(err) {
            if(err) {
                console.log(err);
            } else {
                console.log(name);
            }
        });
    })(files[i][0], files[i][1], files[i][2]);
}
