using System;
using System.Collections.Generic;
using MathNet.Symbolics;

public class Program
{
    public static void Main()
    {
        // Number of program 1
        var squareRoot2 = Math.Sqrt(2);
        Console.WriteLine(squareRoot2);

        // Number of program 2
        var half = new Rational(1, 2);
        var third = new Rational(1, 3);
        var sumResult = half + third;
        Console.WriteLine(sumResult);

        // Number of program 3
        var varX = new Symbol("x");
        var varY = new Symbol("y");
        var binomialExpr = (varX + varY).Pow(6);
        var expandedExpr = binomialExpr.Expand();
        Console.WriteLine(expandedExpr);

        // Number of program 4
        var trigExpr = Math.Sin(varX) / Math.Cos(varX);
        var simplifiedExpr = trigExpr.Simplify();
        Console.WriteLine(simplifiedExpr);

        // Number of program 5
        var equation = (Math.Sin(varX) - varX) / (varX.Pow(3));
        var solutionSet = equation.Solve(varX);
        Console.WriteLine(solutionSet);

        // Number of program 6
        var logExpr = Math.Log(varX);
        var logDerivative = logExpr.Differentiate(varX);
        var logDerivativeValue = logDerivative.Evaluate(varX);
        Console.WriteLine($"Derivative of log(x) with respect to x: {logDerivative}");
        Console.WriteLine($"Value of the derivative: {logDerivativeValue}");

        var inverseExpr = 1 / varX;
        var inverseDerivative = inverseExpr.Differentiate(varX);
        var inverseDerivativeValue = inverseDerivative.Evaluate(varX);
        Console.WriteLine($"Derivative of 1/x with respect to x: {inverseDerivative}");
        Console.WriteLine($"Value of the derivative: {inverseDerivativeValue}");

        var sinExpr = Math.Sin(varX);
        var sinDerivative = sinExpr.Differentiate(varX);
        var sinDerivativeValue = sinDerivative.Evaluate(varX);
        Console.WriteLine($"Derivative of sin(x) with respect to x: {sinDerivative}");
        Console.WriteLine($"Value of the derivative: {sinDerivativeValue}");

        var cosExpr = Math.Cos(varX);
        var cosDerivative = cosExpr.Differentiate(varX);
        var cosDerivativeValue = cosDerivative.Evaluate(varX);
        Console.WriteLine($"Derivative of cos(x) with respect to x: {cosDerivative}");
        Console.WriteLine($"Value of the derivative: {cosDerivativeValue}");

        // Number of program 7
        var equation1 = varX + varY - 2 == 0;
        var equation2 = 2 * varX + varY == 0;
        var solutionDict = VariableSystem.Solve(equation1, equation2, varX, varY);
        Console.WriteLine($"x = {solutionDict[varX]}");
        Console.WriteLine($"y = {solutionDict[varY]}");

        // Number of program 8
        var integratedExpr1 = varX.Pow(2).Integrate(varX);
        Console.WriteLine($"Integration of x^2: {integratedExpr1}");

        var integratedExpr2 = Math.Sin(varX).Integrate(varX);
        Console.WriteLine($"Integration of sin(x): {integratedExpr2}");

        var integratedExpr3 = Math.Cos(varX).Integrate(varX);
        Console.WriteLine($"Integration of cos(x): {integratedExpr3}");

        // Number of program 9
        var functionF = new Function("f")(varX);
        var differentialEquation = functionF.Differentiate(varX, varX) + 9 * functionF == 1;
        var solution = Ode.Solve(differentialEquation, functionF);
        Console.WriteLine(solution);

        // Number of program 10
        var coefficientMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 3, 7, -12 },
            { 4, -2, -5 }
        });
        var constants = Vector<double>.Build.Dense(new double[] { 0, 0 });
        try
        {
            var solutionVector = coefficientMatrix.Solve(constants);
            var xValue = solutionVector[0];
            var yValue = solutionVector[1];
            var zValue = solutionVector[2];
            Console.WriteLine($"x = {xValue}");
            Console.WriteLine($"y = {yValue}");
            Console.WriteLine($"z = {zValue}");
        }
        catch (Exception e)
        {
            Console.WriteLine("No unique solution exists for the system of equations.");
        }
    }
}
