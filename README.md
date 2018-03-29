some codes learning how to do crytographic coding in different environments, ubuntu cmdline, C# .NET Core, golang, nodejs


    //compute the hash of the data in C# .NET Core running in Ubuntu 17.10
    //same as echo -n abc | sha256sum
    ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad
    
    
    dotnet run
    BA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD


    test with https://www.fileformat.info/tool/hash.htm
    and the data: abc
    
    
    
    GenerateSHA256String from abc
BA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD

GenerateSHA256String from abc with base64encoding
ungWv48Bz+pBQUDeXa4iI7ADYaOWF3qctBD/YfIAFa0=

GenerateHMACString from abc w/ key 'thekey'
F8A8DEFFC8B279CC091AB5C1512B1D042D416E246EEBFA2A65B45AB4C5232546

GenerateHMACString from abc  w/ key 'thekey' with base64encoding
+Kje/8iyecwJGrXBUSsdBC1BbiRu6/oqZbRatMUjJUY=
    


