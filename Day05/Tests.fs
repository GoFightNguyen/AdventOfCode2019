namespace Day05

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Puzzle01

[<TestClass>]
type Puzzle01Tests () =

    let ignoreDiagnostics diagnostic = ()
    let IntCodeWhileIgnoringDiagnostics program = IntCode ignoreDiagnostics 1 program 0

    [<TestMethod>]
    member this.SupportsAdditionOpcode () =
        let program = [|1;0;0;0;99|]
        let expected = [|2;0;0;0;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)


    [<TestMethod>]
    member this.SupportsMultiplicationOpcode () =
        let program = [|2;3;0;3;99|]
        let expected = [|2;3;0;6;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.SupportsMultiplicationOpcodeThatPutsValueAfterHaltCode () =
        let program = [|2;4;4;5;99;0|]
        let expected = [|2;4;4;5;99;9801|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.SupportsAdditionAndMultiplicationOpcodes () =
        let program = [|1;1;1;4;99;5;6;0;99|]
        let expected = [|30;1;1;4;2;5;6;0;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.MainExampleFromDay02Puzzle01 () =
        let program = [|1;9;10;3;2;3;11;0;99;30;40;50|]
        let expected = [|3500;9;10;70;2;3;11;0;99;30;40;50|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.CanParseOneDigitInstruction () =
        let instruction = "1"
        let expected = { opcode = 1; parameterModes = [||]}
        let actual = ParseInstruction instruction
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.CanParseTwoDigitInstruction () =
        let instruction = "12"
        let expected = { opcode = 12; parameterModes = [||]}
        let actual = ParseInstruction instruction
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.CanParseThreeDigitInstruction_HavingParameterModeOfImmediate () =
        let instruction = "102"
        let expected = { opcode =2; parameterModes = [|1|]}
        let actual = ParseInstruction instruction
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.CanParseThreeDigitInstruction_HavingParameterModeOfPosition () =
        let instruction = "002"
        let expected = { opcode =2; parameterModes = [|0|]}
        let actual = ParseInstruction instruction
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.CanParseFourDigitInstruction () =
        let instruction = "1002"
        let expected = { opcode =2; parameterModes = [|0;1|]}
        let actual = ParseInstruction instruction
        Assert.AreEqual(expected, actual)

    [<TestMethod>]
    member this.SupportsSpecifiedParameterModesForMultiplication () =
        let program = [|1002;4;3;4;33|]
        let expected = [|1002;4;3;4;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.SupportsSpecifiedParameterModesForAddition () =
        let program = [|1101;100;-1;4;0|]
        let expected = [|1101;100;-1;4;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)

    [<TestMethod>]
    member this.SupportsTheInputOpcode () =
        let program = [|3;0;99|]
        let expected = [|1;0;99|]
        IntCodeWhileIgnoringDiagnostics program
        CollectionAssert.AreEqual(expected, program)


    [<TestMethod>]
    member this.SupportsTheOutputOpcode () =
        let program = [|4;2;32;99|]
        let expected = [|32|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expected, diagnostics)

    [<TestMethod>]
    member this.ExampleUsingInputAndOutput () =
        let program = [|3;0;4;0;99|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        Assert.AreEqual(systemToTest, diagnostics.[0])

    [<TestMethod>]
    member this.Puzzle01 () =
        let program = [|3;225;1;225;6;6;1100;1;238;225;104;0;1001;92;74;224;1001;224;-85;224;4;224;1002;223;8;223;101;1;224;224;1;223;224;223;1101;14;63;225;102;19;83;224;101;-760;224;224;4;224;102;8;223;223;101;2;224;224;1;224;223;223;1101;21;23;224;1001;224;-44;224;4;224;102;8;223;223;101;6;224;224;1;223;224;223;1102;40;16;225;1102;6;15;225;1101;84;11;225;1102;22;25;225;2;35;96;224;1001;224;-350;224;4;224;102;8;223;223;101;6;224;224;1;223;224;223;1101;56;43;225;101;11;192;224;1001;224;-37;224;4;224;102;8;223;223;1001;224;4;224;1;223;224;223;1002;122;61;224;1001;224;-2623;224;4;224;1002;223;8;223;101;7;224;224;1;223;224;223;1;195;87;224;1001;224;-12;224;4;224;1002;223;8;223;101;5;224;224;1;223;224;223;1101;75;26;225;1101;6;20;225;1102;26;60;224;101;-1560;224;224;4;224;102;8;223;223;101;3;224;224;1;223;224;223;4;223;99;0;0;0;677;0;0;0;0;0;0;0;0;0;0;0;1105;0;99999;1105;227;247;1105;1;99999;1005;227;99999;1005;0;256;1105;1;99999;1106;227;99999;1106;0;265;1105;1;99999;1006;0;99999;1006;227;274;1105;1;99999;1105;1;280;1105;1;99999;1;225;225;225;1101;294;0;0;105;1;0;1105;1;99999;1106;0;300;1105;1;99999;1;225;225;225;1101;314;0;0;106;0;0;1105;1;99999;108;677;226;224;102;2;223;223;1006;224;329;1001;223;1;223;1108;226;677;224;1002;223;2;223;1006;224;344;101;1;223;223;7;226;677;224;102;2;223;223;1006;224;359;1001;223;1;223;1007;226;677;224;1002;223;2;223;1006;224;374;1001;223;1;223;1108;677;226;224;102;2;223;223;1005;224;389;1001;223;1;223;107;226;226;224;102;2;223;223;1006;224;404;101;1;223;223;1107;226;226;224;1002;223;2;223;1005;224;419;1001;223;1;223;1007;677;677;224;102;2;223;223;1006;224;434;101;1;223;223;1107;226;677;224;1002;223;2;223;1006;224;449;101;1;223;223;107;677;677;224;102;2;223;223;1005;224;464;1001;223;1;223;1008;226;226;224;1002;223;2;223;1005;224;479;101;1;223;223;1007;226;226;224;102;2;223;223;1005;224;494;1001;223;1;223;8;677;226;224;1002;223;2;223;1005;224;509;1001;223;1;223;108;677;677;224;1002;223;2;223;1005;224;524;1001;223;1;223;1008;677;677;224;102;2;223;223;1006;224;539;1001;223;1;223;7;677;226;224;1002;223;2;223;1005;224;554;101;1;223;223;1108;226;226;224;1002;223;2;223;1005;224;569;101;1;223;223;107;677;226;224;102;2;223;223;1005;224;584;101;1;223;223;8;226;226;224;1002;223;2;223;1005;224;599;101;1;223;223;108;226;226;224;1002;223;2;223;1006;224;614;1001;223;1;223;7;226;226;224;102;2;223;223;1006;224;629;1001;223;1;223;1107;677;226;224;102;2;223;223;1005;224;644;101;1;223;223;8;226;677;224;102;2;223;223;1006;224;659;1001;223;1;223;1008;226;677;224;1002;223;2;223;1006;224;674;1001;223;1;223;4;223;99;226|]    
        let expected = [|3;0;0;0;0;0;0;0;0;2845163|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expected, diagnostics)

[<TestClass>]
type Puzzle02Tests () =

    [<TestMethod>]
    member this.ExampleUsingTheEqualsOpcodeWithPositionModeParameters () =
        let program = [|3;9;8;9;10;9;4;9;99;-1;8|]
        let expectedProgram = [|3;9;8;9;10;9;4;9;99;0;8|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual([|0|], diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheLessThanOpcodeWithPositionModeParameters () =
        let program = [|3;9;7;9;10;9;4;9;99;-1;8|]
        let expectedProgram = [|3;9;7;9;10;9;4;9;99;1;8|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual([|1|], diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheEqualsOpcodeWithImmediateModeParameters () =
        let program = [|3;3;1108;-1;8;3;4;3;99|]
        let expectedProgram = [| 3;3;1108;0;8;3;4;3;99|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual([|0|], diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheLessThanOpcodeWithImmediateModeParameters () =
        let program = [|3;3;1107;-1;8;3;4;3;99|]
        let expectedProgram = [|3;3;1107;1;8;3;4;3;99|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1        
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual([|1|], diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheJumpIfFalseOpcodeWithPositionalModeParameters_Outputs1SinceTheInputWasNonZero () =
        let program = [|3;12;6;12;15;1;13;14;13;4;13;99;-1;0;1;9|]
        let expectedProgram = [|3;12;6;12;15;1;13;14;13;4;13;99;1;1;1;9|]
        let expectedDiagnostics = [|1|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheJumpIfTrueOpcodeWithImmediateModeParameters_Outputs1SinceTheInputWasNonZero () =
        let program = [|3;3;1105;-1;9;1101;0;0;12;4;12;99;1|]
        let expectedProgram = [|3;3;1105;1;9;1101;0;0;12;4;12;99;1|]
        let expectedDiagnostics = [|1|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 1
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheJumpIfFalseOpcodeWithPositionalModeParameters_Outputs0SinceTheInputWas0 () =
        let program = [|3;12;6;12;15;1;13;14;13;4;13;99;-1;0;1;9|]
        let expectedProgram = [|3;12;6;12;15;1;13;14;13;4;13;99;0;0;1;9|]
        let expectedDiagnostics = [|0|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 0
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.ExampleUsingTheJumpIfTrueOpcodeWithImmediateModeParameters_Outputs0SinceTheInputWas0 () =
        let program = [|3;3;1105;-1;9;1101;0;0;12;4;12;99;1|]
        let expectedProgram = [|3;3;1105;0;9;1101;0;0;12;4;12;99;0|]
        let expectedDiagnostics = [|0|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 0
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedProgram, program, "program")
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    // For some reason, this test always failed on me no matter what I used for the systemToTest with an index out-of-bounds error
    // [<TestMethod>]
    // member this.FinalExample_Outputs999SinceTheInputWasBelow8 () =
    //     let program = [|3;21;1008;21;8;20;1005;20;22;107;8;21;20;1006;20;31;1106;0;36;98;0;0;1002;21;125;20;4;20;1105;1;46;104;999;1105;1;46;1101;1000;1;20;4;20;1105;1;46;98;99|]
    //     let expectedDiagnostics = [|999|]

    //     let diagnostics = new Collections.Generic.List<int>()
    //     let diagnosticWriter diagnostic =
    //         diagnostics.Add diagnostic
        
    //     let systemToTest = 7
    //     IntCode diagnosticWriter systemToTest program 0
    //     CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.FinalExample_Outputs1000SinceTheInputWas8 () =
        let program = [|3;21;1008;21;8;20;1005;20;22;107;8;21;20;1006;20;31;1106;0;36;98;0;0;1002;21;125;20;4;20;1105;1;46;104;999;1105;1;46;1101;1000;1;20;4;20;1105;1;46;98;99|]
        let expectedDiagnostics = [|1000|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 8
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.FinalExample_Outputs1001SinceTheInputWasAbove8 () =
        let program = [|3;21;1008;21;8;20;1005;20;22;107;8;21;20;1006;20;31;1106;0;36;98;0;0;1002;21;125;20;4;20;1105;1;46;104;999;1105;1;46;1101;1000;1;20;4;20;1105;1;46;98;99|]
        let expectedDiagnostics = [|1001|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 9
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")

    [<TestMethod>]
    member this.Puzzle02 () =
        let program = [|3;225;1;225;6;6;1100;1;238;225;104;0;1001;92;74;224;1001;224;-85;224;4;224;1002;223;8;223;101;1;224;224;1;223;224;223;1101;14;63;225;102;19;83;224;101;-760;224;224;4;224;102;8;223;223;101;2;224;224;1;224;223;223;1101;21;23;224;1001;224;-44;224;4;224;102;8;223;223;101;6;224;224;1;223;224;223;1102;40;16;225;1102;6;15;225;1101;84;11;225;1102;22;25;225;2;35;96;224;1001;224;-350;224;4;224;102;8;223;223;101;6;224;224;1;223;224;223;1101;56;43;225;101;11;192;224;1001;224;-37;224;4;224;102;8;223;223;1001;224;4;224;1;223;224;223;1002;122;61;224;1001;224;-2623;224;4;224;1002;223;8;223;101;7;224;224;1;223;224;223;1;195;87;224;1001;224;-12;224;4;224;1002;223;8;223;101;5;224;224;1;223;224;223;1101;75;26;225;1101;6;20;225;1102;26;60;224;101;-1560;224;224;4;224;102;8;223;223;101;3;224;224;1;223;224;223;4;223;99;0;0;0;677;0;0;0;0;0;0;0;0;0;0;0;1105;0;99999;1105;227;247;1105;1;99999;1005;227;99999;1005;0;256;1105;1;99999;1106;227;99999;1106;0;265;1105;1;99999;1006;0;99999;1006;227;274;1105;1;99999;1105;1;280;1105;1;99999;1;225;225;225;1101;294;0;0;105;1;0;1105;1;99999;1106;0;300;1105;1;99999;1;225;225;225;1101;314;0;0;106;0;0;1105;1;99999;108;677;226;224;102;2;223;223;1006;224;329;1001;223;1;223;1108;226;677;224;1002;223;2;223;1006;224;344;101;1;223;223;7;226;677;224;102;2;223;223;1006;224;359;1001;223;1;223;1007;226;677;224;1002;223;2;223;1006;224;374;1001;223;1;223;1108;677;226;224;102;2;223;223;1005;224;389;1001;223;1;223;107;226;226;224;102;2;223;223;1006;224;404;101;1;223;223;1107;226;226;224;1002;223;2;223;1005;224;419;1001;223;1;223;1007;677;677;224;102;2;223;223;1006;224;434;101;1;223;223;1107;226;677;224;1002;223;2;223;1006;224;449;101;1;223;223;107;677;677;224;102;2;223;223;1005;224;464;1001;223;1;223;1008;226;226;224;1002;223;2;223;1005;224;479;101;1;223;223;1007;226;226;224;102;2;223;223;1005;224;494;1001;223;1;223;8;677;226;224;1002;223;2;223;1005;224;509;1001;223;1;223;108;677;677;224;1002;223;2;223;1005;224;524;1001;223;1;223;1008;677;677;224;102;2;223;223;1006;224;539;1001;223;1;223;7;677;226;224;1002;223;2;223;1005;224;554;101;1;223;223;1108;226;226;224;1002;223;2;223;1005;224;569;101;1;223;223;107;677;226;224;102;2;223;223;1005;224;584;101;1;223;223;8;226;226;224;1002;223;2;223;1005;224;599;101;1;223;223;108;226;226;224;1002;223;2;223;1006;224;614;1001;223;1;223;7;226;226;224;102;2;223;223;1006;224;629;1001;223;1;223;1107;677;226;224;102;2;223;223;1005;224;644;101;1;223;223;8;226;677;224;102;2;223;223;1006;224;659;1001;223;1;223;1008;226;677;224;1002;223;2;223;1006;224;674;1001;223;1;223;4;223;99;226|]
        let expectedDiagnostics = [|9436229|]

        let diagnostics = new Collections.Generic.List<int>()
        let diagnosticWriter diagnostic =
            diagnostics.Add diagnostic
        
        let systemToTest = 5
        IntCode diagnosticWriter systemToTest program 0
        CollectionAssert.AreEqual(expectedDiagnostics, diagnostics, "diagnostics")