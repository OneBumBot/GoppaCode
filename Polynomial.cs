using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    internal sealed class Polynomial
    {
        internal int[] Coefficients => coefficients;
        internal int Degree => coefficients.Length - 1;
        internal bool IsZero => coefficients[0] == 0;

        internal LinearGoppaCode LinearGoppaCode
        {
            get => default;
            set
            {
            }
        }

        private readonly GF field;
        private readonly int[] coefficients;


        internal Polynomial(GF field, int[] coefficients)
        {
            if (coefficients.Length == 0)
                throw new ArgumentException();

            this.field = field;

            int coefficientsLength = coefficients.Length;
            if (coefficientsLength > 1 && coefficients[0] == 0)
            {

                int firstNonZero = 1;

                while (firstNonZero < coefficientsLength && coefficients[firstNonZero] == 0)
                    firstNonZero++;

                if (firstNonZero == coefficientsLength)
                    this.coefficients = field.Zero.coefficients;
                else
                {
                    this.coefficients = new int[coefficientsLength - firstNonZero];
                    Array.Copy(coefficients,
                        firstNonZero,
                        this.coefficients,
                        0,
                        this.coefficients.Length);
                }
            }
            else
                this.coefficients = coefficients;
        }

        internal int GetCoefficient(int degree)
        {
            return coefficients[coefficients.Length - 1 - degree];
        }


        internal int EvaluateAt(int a)
        {
            int result = 0;

            if (a == 0)
            {
                return GetCoefficient(0);
            }

            int size = coefficients.Length;

            if (a == 1)
            {
                foreach (var coefficient in coefficients)
                    result = GF.AddOrSubtract(result, coefficient);

                return result;
            }

            result = coefficients[0];

            for (int i = 1; i < size; i++)
                result = GF.AddOrSubtract(field.Multiply(a, result), coefficients[i]);

            return result;
        }

        internal Polynomial AddOrSubtract(Polynomial other)
        {
            if (field.Equals(other.field) == false)
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");

            if (IsZero)
                return other;

            if (other.IsZero)
                return this;

            int[] smallerCoefficients = coefficients;
            int[] largerCoefficients = other.coefficients;

            if (smallerCoefficients.Length > largerCoefficients.Length)
            {
                int[] temp = smallerCoefficients;
                smallerCoefficients = largerCoefficients;
                largerCoefficients = temp;
            }

            int[] sumDiff = new int[largerCoefficients.Length];
            int lengthDiff = largerCoefficients.Length - smallerCoefficients.Length;

            Array.Copy(largerCoefficients, 0, sumDiff, 0, lengthDiff);

            for (int i = lengthDiff; i < largerCoefficients.Length; i++)
                sumDiff[i] = GF.AddOrSubtract(smallerCoefficients[i - lengthDiff], largerCoefficients[i]);

            return new Polynomial(field, sumDiff);
        }

        internal Polynomial Multiply(Polynomial other)
        {
            if (field.Equals(other.field) == false)
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");

            if (IsZero || other.IsZero)
                return field.Zero;

            int[] aCoefficients = coefficients;
            int aLength = aCoefficients.Length;
            int[] bCoefficients = other.coefficients;
            int bLength = bCoefficients.Length;
            int[] product = new int[aLength + bLength - 1];

            for (int i = 0; i < aLength; i++)
            {
                int aCoeff = aCoefficients[i];

                for (int j = 0; j < bLength; j++)
                    product[i + j] = GF.AddOrSubtract(product[i + j], field.Multiply(aCoeff, bCoefficients[j]));
            }
            return new Polynomial(field, product);
        }

        internal Polynomial Multiply(int scalar)
        {
            if (scalar == 0)
                return field.Zero;

            if (scalar == 1)
                return this;

            int size = coefficients.Length;
            int[] product = new int[size];

            for (int i = 0; i < size; i++)
                product[i] = field.Multiply(coefficients[i], scalar);

            return new Polynomial(field, product);
        }

        internal Polynomial MultiplyByMonomial(int degree, int coefficient)
        {
            if (degree < 0)
                throw new ArgumentException();

            if (coefficient == 0)
                return field.Zero;

            int size = coefficients.Length;
            int[] product = new int[size + degree];

            for (int i = 0; i < size; i++)
                product[i] = field.Multiply(coefficients[i], coefficient);

            return new Polynomial(field, product);
        }

        internal Polynomial[] Divide(Polynomial other)
        {
            if (field.Equals(other.field) == false)
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");

            if (other.IsZero)
                throw new ArgumentException("Divide by 0");

            Polynomial quotient = field.Zero;
            Polynomial remainder = this;

            int denominatorLeadingTerm = other.GetCoefficient(other.Degree);
            int inverseDenominatorLeadingTerm = field.Inverse(denominatorLeadingTerm);

            while (remainder.Degree >= other.Degree && !remainder.IsZero)
            {
                int degreeDifference = remainder.Degree - other.Degree;
                int scale = field.Multiply(remainder.GetCoefficient(remainder.Degree), inverseDenominatorLeadingTerm);
                Polynomial term = other.MultiplyByMonomial(degreeDifference, scale);
                Polynomial iterationQuotient = field.BuildMonomial(degreeDifference, scale);

                quotient = quotient.AddOrSubtract(iterationQuotient);
                remainder = remainder.AddOrSubtract(term);
            }

            return new Polynomial[] { quotient, remainder };
        }

        public override String ToString()
        {
            var result = new StringBuilder(8 * Degree);

            for (int degree = Degree; degree >= 0; degree--)
            {
                int coefficient = GetCoefficient(degree);

                if (coefficient != 0)
                {
                    if (coefficient < 0)
                    {
                        result.Append(" - ");
                        coefficient = -coefficient;
                    }
                    else
                    {
                        if (result.Length > 0)
                            result.Append(" + ");
                    }

                    if (degree == 0 || coefficient != 1)
                    {
                        int alphaPower = field.Log(coefficient);

                        if (alphaPower == 0)
                            result.Append('1');
                        else if (alphaPower == 1)
                            result.Append('a');
                        else
                        {
                            result.Append("a^");
                            result.Append(alphaPower);
                        }
                    }

                    if (degree != 0)
                    {
                        if (degree == 1)
                            result.Append('x');
                        else
                        {
                            result.Append("x^");
                            result.Append(degree);
                        }
                    }
                }
            }

            return result.ToString();
        }
    }
}
