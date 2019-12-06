module Puzzle01

type Instruction = {
    opcode : int
    parameterModes : int[]
}

let ParseInstructionContainingSpecifiedParameterModes (instruction:string) =
    let startingIndexOfOpcode = instruction.Length - 2
    let opcode = int instruction.[startingIndexOfOpcode..]
    let parameterModes = 
        instruction.[0..startingIndexOfOpcode-1].ToCharArray() 
        |> Array.map (string >> int) 
        |> Array.rev
    {
        opcode = opcode
        parameterModes = parameterModes
    }

let ParseInstruction instruction =
    let instructionString = instruction.ToString()
    if instructionString.Length <= 2 then
        {
            opcode = int instructionString
            parameterModes = [||]
        }
    else
        ParseInstructionContainingSpecifiedParameterModes instructionString

let IsParameterModePositional instruction index =
    let parameterMode = instruction.parameterModes |> Array.tryItem index
    parameterMode.IsNone || parameterMode.Value = 0

let GetParametersForMathOperations (program:int[]) instruction instructionPointer =
    let parameter1 =
        let addressOrValue = program.[instructionPointer + 1] 
        if IsParameterModePositional instruction 0 then
            program.[addressOrValue]
        else // immediate value
            addressOrValue

    let parameter2 =
        let addressOrValue = program.[instructionPointer + 2] 
        if IsParameterModePositional instruction 1 then
            program.[addressOrValue]
        else // immediate value
            addressOrValue

    parameter1, parameter2        

let rec IntCode diagnosticWriter systemToTest (program:int[]) instructionPointer =
    let instruction = program.[instructionPointer] |> ParseInstruction
    let opcode = instruction.opcode
    let IntCodeTemplate = IntCode diagnosticWriter systemToTest

    if opcode = 1 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        let output = parameter1 + parameter2
        let outputAddress = program.[instructionPointer + 3]
        program.[outputAddress] <- output

        IntCodeTemplate program (instructionPointer + 4)
    else if opcode = 2 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        let output = parameter1 * parameter2
        let outputAddress = program.[instructionPointer + 3]
        program.[outputAddress] <- output    

        IntCodeTemplate program (instructionPointer + 4)

    else if opcode = 3 then
        let address = program.[instructionPointer + 1]
        program.[address] <- systemToTest

        IntCodeTemplate program (instructionPointer + 2)

    else if opcode = 4 then
        let address = program.[instructionPointer + 1]
        let diagnostic = program.[address]
        diagnosticWriter diagnostic

        IntCodeTemplate program (instructionPointer + 2)

    else if opcode = 5 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        if parameter1 <> 0 then
            IntCodeTemplate program parameter2
        else
            IntCodeTemplate program (instructionPointer + 3)

    else if opcode = 6 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        if parameter1 = 0 then
            IntCodeTemplate program parameter2
        else
            IntCodeTemplate program (instructionPointer + 3)

    else if opcode = 7 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        let address = program.[instructionPointer + 3]
        if parameter1 < parameter2 then
            program.[address] <- 1
        else 
            program.[address] <- 0

        IntCodeTemplate program (instructionPointer + 4)

    else if opcode = 8 then
        let parameter1, parameter2 = GetParametersForMathOperations program instruction instructionPointer
        let address = program.[instructionPointer + 3]
        if parameter1 = parameter2 then
            program.[address] <- 1
        else 
            program.[address] <- 0

        IntCodeTemplate program (instructionPointer + 4)