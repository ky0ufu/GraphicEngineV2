﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEngineV2
{
    public class VectorSpace
    {
        public Vector[] Basis { get; set; }

        public VectorSpace(Vector[] basis)
        {
            if (basis == null)
                VectorSpaceException.InitError();
            int size = basis.Length;
            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i].VectorSize() != size)
                    VectorSpaceException.NotBasis();
                if (basis[i].IsTranspose)
                    VectorException.FormException();
            }
            Basis = basis;
        }

        public float ScalarProduct(Vector vec1, Vector vec2)
        {
            vec1 = Vector.Transpose(Vector.ToNotTransposeVector(vec1));
            vec2 = Vector.ToNotTransposeVector(vec2);
            return (Vector.VectorToMatrix(vec1) * Matrix.Gram(Basis) * Vector.VectorToMatrix(vec2)).data[0, 0];
        }

        public Vector VectorProduct(Vector vec1, Vector vec2)
        {
            if (Basis.Length != 3)
                VectorSpaceException.BadBasisSize();
            Vector result = new Vector(new float[3, 1]);
            for (int i = 0; i < 3; i++)
                result += Basis[i] * Vector.VectorProduct(vec1, vec2)[i];
            return result;
        }
        public Vector AsVector(Point point)
        {
            if (point.PointSize() != Basis.Length)
                throw new EngineException("Point size not equal basis length");
            Vector vec = new Vector(new float[point.PointSize(), 1]);
            {
                for (int j = 0; j < point.PointSize(); j++)
                    for (int i = 0; i < point.PointSize(); i++)
                    {
                        vec.data[j, 0] += point.point[i] * Basis[j].data[i, 0];
                    }
            }
            return vec;
        }
    }
}