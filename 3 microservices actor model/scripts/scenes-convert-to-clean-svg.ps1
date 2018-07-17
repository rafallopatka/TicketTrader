$scenesDirectory = 'resources/scenes/';
$inkspace = $env:programfiles + '\Inkscape\inkscape.exe';

$filesToConvert = $(Get-ChildItem resources\scenes\ -Filter *orig.svg -Recurse | % { $_.FullName });

Foreach ($file in $filesToConvert)
{
	echo $file
	$fileClean = $file -replace 'orig.svg', 'clean.svg'
	& $inkspace --export-plain-svg=$fileClean $file;
}

echo 'completed'