# Deploys the build to the workshop folder.
param (
    [string]$BuildPath,
    [string]$ProjectDirectory,
    [string]$WorkshopPath
)

$isBeta = $false

if($ProjectDirectory -like "%beta"){
    $isBeta = $true
}




