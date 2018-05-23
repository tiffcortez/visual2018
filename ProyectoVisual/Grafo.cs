﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace ProyectoVisual
{
    class Grafo
    {
        private List<Vertice> vertices;
        private List<Arista> aristas;
        private int idv, ida; //Identificador para los vértices y las aristas

        //selecciones
        private Vertice vselec;

        public Grafo()
        {
            vertices = new List<Vertice>();
            aristas = new List<Arista>();
            idv = 0;
            ida = 0;
        }

        //getters setters
        public List<Vertice> Vertices
        {
            get
            {
                return vertices;
            }
            set
            {
                vertices = value;
            }
        }
        public List<Arista> Aristas
        {
            get
            {
                return aristas;
            }
            set
            {
                aristas = value;
            }
        }
        public int IDV
        {
            get { return idv; }
            set { idv = value; }
        }
        public int IDA
        {
            get { return ida; }
            set { ida = value; }
        }

        public void AgregaVertice(Graphics g, int x, int y, int width, int height)
        {
            bool banColision = false;
            Vertice v = new Vertice(idv, x, y);

            foreach(Vertice ver in vertices)
            {
                banColision = x-v.Radio <= ver.X + ver.Radio && x+ v.Radio >= ver.X-ver.Radio && y- v.Radio <= ver.Y + ver.Radio && y+ v.Radio >= ver.Y-ver.Radio;
                if (banColision)
                    return;
            }
                
            

            if (!banColision && v.ChecarLimites(x, y, width, height))
            {
                idv++;
                vertices.Add(v);
                v.Dibujar(g);
            }
        }

        //SELECCIONAR VERTICE
        public void SeleccionarVertice(Graphics g, int vid)
        {
            vselec = vertices[vid];
            vselec.Seleccionar(g);
        }
        
        //MOVER VERTICE
        public void MoverVertice(Graphics g, int xmouse, int ymouse, int width, int height)
        {
            bool inicio;
            if (vselec != null&&vselec.ChecarLimites(xmouse, ymouse,width,height))
            {
                vselec.X = xmouse;
                vselec.Y = ymouse;

                foreach (Arista a in aristas)
                {
                    if (a.ChecarVertice(vselec))
                    {
                        inicio=vselec.ID == a.IDV1;
                        if (inicio)
                        {
                            a.X1 = vselec.X;
                            a.Y1 = vselec.Y;
                        }
                        else
                        {
                            a.X2 = vselec.X;
                            a.Y2 = vselec.Y;
                        }
                    }
                }

                Dibujar(g);

            }

        }

        //AGREGAR ARISTA
        public void AgregaArista(Graphics g, Vertice v1, Vertice v2)
        {
            
            try
            {
               
                Arista a = new Arista(ida,v1.ID, v2.ID, v1.X, v1.Y, v2.X, v2.Y);
                aristas.Add(a);

                a.DibujaArista(g);

                ida++;
            }catch(Exception ex)
            {
                Console.WriteLine("No se puede");
            }

        }
        
        //DIBUJAR GRAFO
        public void Dibujar(Graphics g)
        {
            g.Clear(Color.LightGray);
            if(vertices.Count>0)
                foreach (Vertice v in vertices)
                    v.Dibujar(g);

            if(aristas.Count>0)
                foreach (Arista a in aristas)
                    a.DibujaArista(g);
        }

        public void destruir()
        {
            if(vertices.Count>0)
                vertices.Clear();
            if(aristas.Count>0)
                aristas.Clear();
            idv = 0;
            ida = 0;
        }

        public void copiar(Grafo g)
        {
            g.idv = idv;
            g.ida = ida;

            foreach(Vertice v in vertices)
            {
                g.vertices.Add(v);
            }

            foreach(Arista a in aristas)
            {
                g.aristas.Add(a);
            }
        }
    }
}