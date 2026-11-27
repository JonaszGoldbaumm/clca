$p = $PWD
cd $Script:PSScriptRoot
$srcF = $(gci . -Filter "*.f90")
echo "Found $($srcF.Length) library sources:"
foreach ($file in $srcF) {
    echo ">>$($file.BaseName)"
}

gfortran -shared -o MathBase.dll @srcF
if (!($LastExitCode -eq 0)) {
    echo "Compilation failed with exitcode: $LastExitCode"
    return -1
}
ri *.mod
cd $p