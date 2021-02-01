# BowlingScore

## Environment requirements:
- .NET Core 3.1 runtime 

## How to run the app
- navigate to the **BowlingScore** folder:
```
BowlingScore
└── BowlingScore.Tests
└── BolingScore     <-
└── .gitignore
└── BowlingScore.sln
└── README.md
```
 - run the following command:
 
    `dotnet run --input input.csv`
    
    **_Important:_** `--input pathToInput.csv/txt` is required. The input file must be in `\BowlingScore\bin\Debug\netcoreapp3.1\`. Otherwise, you must provide the relative/absolute path. If this folder does not exist, you can run `dotnet build` to generate it.
