# Ray-Casting Graphic Engine on C#
## **About Project** ##
***
### This is study project ###
***
## **Requirements** ##
### .Net 4.8 framework ###
***
## **Classes and methods** ##
***
## **class Matrix** ##
### **Initialization** ###
Possible variants for initialization

Create matrix from two-dimensional array
```

        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
        }
```
Create matrix N x M and filling zero

        public Matrix(int n, int m)
        {
            matrix = new float[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    matrix[i, j] = 0;
        }
Create matrix N x N and filling zero
```
        public Matrix(int n): this(n, n)
        {
        }
```
### **Properties** ###
` float [,] matrix `  - matrix data
***
### **Methods** ###
`Rows()` - return (`int`) count of rows in matrix \
`Cols()` - return (`int`) count of cols in matrix \
`Transpose()` - return (`Matrix`) transposed matrix \
`Determinant()` return (`float`) Determinant of matrix NxN \
`Matrix.Inverse(Matrix matrix)` - static method, return (`Matrix`) inverse matrix \
`Matrix.Identity(int n)` - static method, return (`Matrix`) Identity matrix size NxN\
`Matrix.Gram(Vector[] Basis) ` - static method, return (`Matrix`) Gram matrix
### **Overloads** ###
`Matrix + Matrix` - return `Matrix`; matrix addition operator\
`Matrix * Matrix` - return `Matrix`; matrix multiplication operator\
`Matrix * (float)scalar` and `(float)scalar * Matrix` - return `Matrix`; multiplicate Matrix on scalar
### Examples ###
Create matrix
```
float[,] arr =  { {1, 0, 0 },
                  {0, 1, 0 },
                  {0, 0, 1 } };
float[,] arr1 = { {1, 2, 3 },
                  {4, 5, 6 },
                  {7, 8, 9 } };
            Matrix matrix1 = new Matrix(arr);
            Matrix matrix2 = new Matrix(arr1);
```
Addition
```
float[,] arr =  { {1, 0, 0 },
                  {0, 1, 0 },
                  {0, 0, 1 } };
float[,] arr1 = { {1, 2, 3 },
                  {4, 5, 6 },
                  {7, 8, 9 } };
            Matrix matrix1 = new Matrix(arr);
            Matrix matrix2 = new Matrix(arr1);
Matrix result = matrix1 + matrix2;
```
Result
```
2 2 3
4 6 6
7 8 10
