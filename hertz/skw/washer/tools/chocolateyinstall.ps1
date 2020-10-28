
$zipurl = $PSScriptRoot + "\hertzsetting.xml"
$configDir = 'C:\Aucxis\Run\Hertz\Config\'

if(-not (Test-Path  -Path $configDir)){
	New-Item -ItemType Directory -Path $configDir
}
Copy-Item $zipurl -Destination $configDir -Verbose
