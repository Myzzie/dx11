﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Camera;

namespace CameraDemo {
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    using Core;
    using Core.FX;

    using SlimDX;
    using SlimDX.Direct3D11;
    using SlimDX.DXGI;

    public class CameraDemo : D3DApp {
        private Buffer _shapesVB;
        private Buffer _shapesIB;

        private Buffer _skullVB;
        private Buffer _skullIB;

        private ShaderResourceView _floorTexSRV;
        private ShaderResourceView _stoneTexSRV;
        private ShaderResourceView _brickTexSRV;

        private readonly DirectionalLight[] _dirLights;
        private readonly Material _gridMat;
        private readonly Material _boxMat;
        private readonly Material _cylinderMat;
        private readonly Material _sphereMat;
        private readonly Material _skullMat;

        private readonly Matrix[] _sphereWorld = new Matrix[10];
        private readonly Matrix[] _cylWorld = new Matrix[10];
        private readonly Matrix _boxWorld;
        private readonly Matrix _gridWorld;
        private readonly Matrix _skullWorld;

        private int _boxVertexOffset;
        private int _gridVertexOffset;
        private int _sphereVertexOffset;
        private int _cylinderVertexOffset;

        private int _boxIndexOffset;
        private int _gridIndexOffset;
        private int _sphereIndexOffset;
        private int _cylinderIndexOffset;

        private int _boxIndexCount;
        private int _gridIndexCount;
        private int _sphereIndexCount;
        private int _cylinderIndexCount;
        private int _skullIndexCount;

        private int _lightCount;
        private readonly FpsCamera _cam;
        private readonly LookAtCamera _cam2;
        private bool _useFpsCamera;

        private Point _lastMousePos;
        private bool _disposed;

        public CameraDemo(IntPtr hInstance) : base(hInstance) {
            _lightCount = 3;
            Enable4xMsaa = true;
            MainWindowCaption = "Camera Demo";

            _lastMousePos = new Point();

            _useFpsCamera = true;

            _cam = new FpsCamera {
                Position = new Vector3(0, 2, -15)
            };

            _cam2 = new LookAtCamera();
            _cam2.LookAt( new Vector3(0, 2, -15),new Vector3(), Vector3.UnitY );

            _gridWorld = Matrix.Identity;

            _boxWorld = Matrix.Scaling(3.0f, 1.0f, 3.0f) * Matrix.Translation(0, 0.5f, 0);

            _skullWorld = Matrix.Scaling(0.5f, 0.5f, 0.5f) * Matrix.Translation(0, 1.0f, 0);
            for (var i = 0; i < 5; i++) {
                _cylWorld[i * 2] = Matrix.Translation(-5.0f, 1.5f, -10.0f + i * 5.0f);
                _cylWorld[i * 2 + 1] = Matrix.Translation(5.0f, 1.5f, -10.0f + i * 5.0f);

                _sphereWorld[i * 2] = Matrix.Translation(-5.0f, 3.5f, -10.0f + i * 5.0f);
                _sphereWorld[i * 2 + 1] = Matrix.Translation(5.0f, 3.5f, -10.0f + i * 5.0f);
            }
            _dirLights = new[] {
                new DirectionalLight {
                    Ambient = new Color4( 0.2f, 0.2f, 0.2f),
                    Diffuse = new Color4(0.5f, 0.5f, 0.5f),
                    Specular = new Color4(0.5f, 0.5f, 0.5f),
                    Direction = new Vector3(0.57735f, -0.57735f, 0.57735f)
                },
                new DirectionalLight {
                    Ambient = new Color4(0,0,0),
                    Diffuse = new Color4(1.0f, 0.2f, 0.2f, 0.2f),
                    Specular = new Color4(1.0f, 0.25f, 0.25f, 0.25f),
                    Direction = new Vector3(-0.57735f, -0.57735f, 0.57735f)
                },
                new DirectionalLight {
                    Ambient = new Color4(0,0,0),
                    Diffuse = new Color4(1.0f,0.2f, 0.2f, 0.2f),
                    Specular = new Color4(0,0,0),
                    Direction = new Vector3(0, -0.707f, -0.707f)
                }
            };
            _gridMat = new Material {
                Ambient = new Color4(0.8f, 0.8f, 0.8f),
                Diffuse = new Color4(0.8f, 0.8f, 0.8f),
                Specular = new Color4(16.0f, 0.8f, 0.8f, 0.8f)
            };
            _cylinderMat = new Material {
                Ambient = Color.White,
                Diffuse = Color.White,
                Specular = new Color4(16.0f, 0.8f, 0.8f, 0.8f)
            };
            _sphereMat = new Material {
                Ambient = new Color4(0.6f, 0.8f, 0.9f),
                Diffuse = new Color4(0.6f, 0.8f, 0.9f),
                Specular = new Color4(16.0f, 0.9f, 0.9f, 0.9f)
            };
            _boxMat = new Material {
                Ambient = Color.White,
                Diffuse = Color.White,
                Specular = new Color4(16.0f, 0.8f, 0.8f, 0.8f)
            };
            _skullMat = new Material {
                Ambient = new Color4(0.4f, 0.4f, 0.4f),
                Diffuse = new Color4(0.8f, 0.8f, 0.8f),
                Specular = new Color4(16.0f, 0.8f, 0.8f, 0.8f)
            };
        }
        protected override void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    ImmediateContext.ClearState();
                    Util.ReleaseCom(ref _shapesVB);
                    Util.ReleaseCom(ref _shapesIB);
                    Util.ReleaseCom(ref _skullVB);
                    Util.ReleaseCom(ref _skullIB);

                    Util.ReleaseCom(ref _floorTexSRV);
                    Util.ReleaseCom(ref _stoneTexSRV);
                    Util.ReleaseCom(ref _brickTexSRV);

                    Effects.DestroyAll();
                    InputLayouts.DestroyAll();
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public override bool Init() {
            if (!base.Init()) return false;

            Effects.InitAll(Device);
            InputLayouts.InitAll(Device);

            _floorTexSRV = ShaderResourceView.FromFile(Device, "Textures/floor.dds");
            _stoneTexSRV = ShaderResourceView.FromFile(Device, "Textures/stone.dds");
            _brickTexSRV = ShaderResourceView.FromFile(Device, "Textures/bricks.dds");

            BuildShapeGeometryBuffers();
            BuildSkullGeometryBuffers();

            Window.KeyDown += SwitchLights;

            return true;
        }
        private void SwitchLights(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.D0:
                    _lightCount = 0;
                    break;
                case Keys.D1:
                    _lightCount = 1;
                    break;
                case Keys.D2:
                    _lightCount = 2;
                    break;
                case Keys.D3:
                    _lightCount = 3;
                    break;
            }
        }

        public override void OnResize() {
            base.OnResize();
            _cam.SetLens(0.25f*MathF.PI, AspectRatio, 1.0f, 1000.0f);
            _cam2.SetLens(0.25f * MathF.PI, AspectRatio, 1.0f, 1000.0f);
        }

        public override void UpdateScene(float dt) {
            base.UpdateScene(dt);

            if (Util.IsKeyDown(Keys.Up)){
                if (_useFpsCamera) {
                    _cam.Walk(10.0f*dt);
                } else {
                    _cam2.Walk(10.0f*dt);
                }
            }
            if (Util.IsKeyDown(Keys.Down)) {
                if (_useFpsCamera) {
                    _cam.Walk(-10.0f*dt);
                } else {
                    _cam2.Walk(-10.0f*dt);
                }
            }

            if (Util.IsKeyDown(Keys.Left)) {
                if (_useFpsCamera) {
                    _cam.Strafe(-10.0f*dt);
                } else {
                    _cam2.Strafe(-10.0f*dt);
                }
            }
            if (Util.IsKeyDown(Keys.Right)) {
                if (_useFpsCamera) {
                    _cam.Strafe(10.0f*dt);
                } else {
                    _cam2.Strafe(10.0f*dt);
                }
            }
            if (Util.IsKeyDown(Keys.L)) {
                _useFpsCamera = false;
            }
            if (Util.IsKeyDown(Keys.F)) {
                _useFpsCamera = true;
            }
            if (!_useFpsCamera) {
                if (Util.IsKeyDown(Keys.PageUp)) {
                    _cam2.Zoom(-10.0f*dt);
                }
                if (Util.IsKeyDown(Keys.PageDown)) {
                    _cam2.Zoom(10.0f * dt);
                }
            }


        }

        public override void DrawScene() {
            ImmediateContext.ClearRenderTargetView(RenderTargetView, Color.Silver);
            ImmediateContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);

            ImmediateContext.InputAssembler.InputLayout = InputLayouts.Basic32;
            ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            Matrix view;
            Matrix proj;
            Matrix viewProj;
            if (_useFpsCamera) {
                _cam.UpdateViewMatrix();

                view = _cam.View;
                proj = _cam.Proj;
                viewProj = _cam.ViewProj;
                Effects.BasicFX.SetEyePosW(_cam.Position);
            } else {
                _cam2.UpdateViewMatrix();

                view = _cam2.View;
                proj = _cam2.Proj;
                viewProj = _cam2.ViewProj;
                Effects.BasicFX.SetEyePosW(_cam2.Position);
            }

            Effects.BasicFX.SetDirLights(_dirLights);
            

            var activeTexTech = Effects.BasicFX.Light1TexTech;
            var activeSkullTech = Effects.BasicFX.Light1Tech;
            switch (_lightCount) {
                case 1:
                    activeTexTech = Effects.BasicFX.Light1TexTech;
                    activeSkullTech = Effects.BasicFX.Light1Tech;
                    break;
                case 2:
                    activeTexTech = Effects.BasicFX.Light2TexTech;
                    activeSkullTech = Effects.BasicFX.Light2Tech;
                    break;
                case 3:
                    activeTexTech = Effects.BasicFX.Light3TexTech;
                    activeSkullTech = Effects.BasicFX.Light3Tech;
                    break;
            }
            for (var p = 0; p < activeTexTech.Description.PassCount; p++) {
                var pass = activeTexTech.GetPassByIndex(p);
                ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_shapesVB, Basic32.Stride, 0));
                ImmediateContext.InputAssembler.SetIndexBuffer(_shapesIB, Format.R32_UInt, 0);

                var world = _gridWorld;
                var worldInvTranspose = MathF.InverseTranspose(world);
                var wvp = world * view*proj;
                Effects.BasicFX.SetWorld(world);
                Effects.BasicFX.SetWorldInvTranspose(worldInvTranspose);
                Effects.BasicFX.SetWorldViewProj(wvp);
                Effects.BasicFX.SetTexTransform(Matrix.Scaling(6, 8, 1));
                Effects.BasicFX.SetMaterial(_gridMat);
                Effects.BasicFX.SetDiffuseMap(_floorTexSRV);
                pass.Apply(ImmediateContext);
                ImmediateContext.DrawIndexed(_gridIndexCount, _gridIndexOffset, _gridVertexOffset);

                world = _boxWorld;
                worldInvTranspose = MathF.InverseTranspose(world);
                wvp = world * viewProj;
                Effects.BasicFX.SetWorld(world);
                Effects.BasicFX.SetWorldInvTranspose(worldInvTranspose);
                Effects.BasicFX.SetWorldViewProj(wvp);
                Effects.BasicFX.SetTexTransform(Matrix.Identity);
                Effects.BasicFX.SetMaterial(_boxMat);
                Effects.BasicFX.SetDiffuseMap(_stoneTexSRV);
                pass.Apply(ImmediateContext);
                ImmediateContext.DrawIndexed(_boxIndexCount, _boxIndexOffset, _boxVertexOffset);

                foreach (var matrix in _cylWorld) {
                    world = matrix;
                    worldInvTranspose = MathF.InverseTranspose(world);
                    wvp = world * viewProj;
                    Effects.BasicFX.SetWorld(world);
                    Effects.BasicFX.SetWorldInvTranspose(worldInvTranspose);
                    Effects.BasicFX.SetWorldViewProj(wvp);
                    Effects.BasicFX.SetTexTransform(Matrix.Identity);
                    Effects.BasicFX.SetMaterial(_cylinderMat);
                    Effects.BasicFX.SetDiffuseMap(_brickTexSRV);
                    pass.Apply(ImmediateContext);
                    ImmediateContext.DrawIndexed(_cylinderIndexCount, _cylinderIndexOffset, _cylinderVertexOffset);
                }
                foreach (var matrix in _sphereWorld) {
                    world = matrix;
                    worldInvTranspose = MathF.InverseTranspose(world);
                    wvp = world * viewProj;
                    Effects.BasicFX.SetWorld(world);
                    Effects.BasicFX.SetWorldInvTranspose(worldInvTranspose);
                    Effects.BasicFX.SetWorldViewProj(wvp);
                    Effects.BasicFX.SetTexTransform(Matrix.Identity);
                    Effects.BasicFX.SetMaterial(_sphereMat);
                    Effects.BasicFX.SetDiffuseMap(_stoneTexSRV);
                    pass.Apply(ImmediateContext);
                    ImmediateContext.DrawIndexed(_sphereIndexCount, _sphereIndexOffset, _sphereVertexOffset);
                }
                
                

            }
            for (int p = 0; p < activeSkullTech.Description.PassCount; p++) {
                var pass = activeSkullTech.GetPassByIndex(p);
                ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_skullVB, Basic32.Stride, 0));
                ImmediateContext.InputAssembler.SetIndexBuffer(_skullIB, Format.R32_UInt, 0);

                var world = _skullWorld;
                var worldInvTranspose = MathF.InverseTranspose(world);
                var wvp = world * viewProj;
                Effects.BasicFX.SetWorld(world);
                Effects.BasicFX.SetWorldInvTranspose(worldInvTranspose);
                Effects.BasicFX.SetWorldViewProj(wvp);
                Effects.BasicFX.SetMaterial(_skullMat);
                pass.Apply(ImmediateContext);
                ImmediateContext.DrawIndexed(_skullIndexCount, 0, 0);
            }
            SwapChain.Present(0, PresentFlags.None);
        }

        protected override void OnMouseDown(object sender, MouseEventArgs mouseEventArgs) {
            _lastMousePos = mouseEventArgs.Location;
            Window.Capture = true;
        }
        protected override void OnMouseUp(object sender, MouseEventArgs e) {
            Window.Capture = false;
        }
        protected override void OnMouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                var dx = MathF.ToRadians(0.25f * (e.X - _lastMousePos.X));
                var dy = MathF.ToRadians(0.25f * (e.Y - _lastMousePos.Y));
                if (_useFpsCamera) {
                    _cam.Pitch(dy);
                    _cam.Yaw(dx);
                } else {
                    _cam2.Pitch(dy);
                    _cam2.Yaw(dx);
                }
            } 
            _lastMousePos = e.Location;
        }

        private void BuildShapeGeometryBuffers() {
            var box = GeometryGenerator.CreateBox(1, 1, 1);
            var grid = GeometryGenerator.CreateGrid(20, 30, 60, 40);
            var sphere = GeometryGenerator.CreateSphere(0.5f, 20, 20);
            var cylinder = GeometryGenerator.CreateCylinder(0.5f, 0.3f, 3.0f, 20, 20);

            _boxVertexOffset = 0;
            _gridVertexOffset = box.Vertices.Count;
            _sphereVertexOffset = _gridVertexOffset + grid.Vertices.Count;
            _cylinderVertexOffset = _sphereVertexOffset + sphere.Vertices.Count;

            _boxIndexCount = box.Indices.Count;
            _gridIndexCount = grid.Indices.Count;
            _sphereIndexCount = sphere.Indices.Count;
            _cylinderIndexCount = cylinder.Indices.Count;

            _boxIndexOffset = 0;
            _gridIndexOffset = _boxIndexCount;
            _sphereIndexOffset = _gridIndexOffset + _gridIndexCount;
            _cylinderIndexOffset = _sphereIndexOffset + _sphereIndexCount;

            var totalVertexCount = box.Vertices.Count + grid.Vertices.Count + sphere.Vertices.Count + cylinder.Vertices.Count;
            var totalIndexCount = _boxIndexCount + _gridIndexCount + _sphereIndexCount + _cylinderIndexCount;

            var vertices = box.Vertices.Select(v => new Basic32(v.Position, v.Normal, v.TexC)).ToList();
            vertices.AddRange(grid.Vertices.Select(v => new Basic32(v.Position, v.Normal, v.TexC)));
            vertices.AddRange(sphere.Vertices.Select(v => new Basic32(v.Position, v.Normal, v.TexC)));
            vertices.AddRange(cylinder.Vertices.Select(v => new Basic32(v.Position, v.Normal, v.TexC)));

            var vbd = new BufferDescription(Basic32.Stride * totalVertexCount, ResourceUsage.Immutable, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            _shapesVB = new Buffer(Device, new DataStream(vertices.ToArray(), false, false), vbd);

            var indices = new List<int>();
            indices.AddRange(box.Indices);
            indices.AddRange(grid.Indices);
            indices.AddRange(sphere.Indices);
            indices.AddRange(cylinder.Indices);

            var ibd = new BufferDescription(sizeof(int) * totalIndexCount, ResourceUsage.Immutable, BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            _shapesIB = new Buffer(Device, new DataStream(indices.ToArray(), false, false), ibd);
        }

        private void BuildSkullGeometryBuffers() {
            try {
                var vertices = new List<Basic32>();
                var indices = new List<int>();
                var vcount = 0;
                var tcount = 0;
                using (var reader = new StreamReader("Models\\skull.txt")) {


                    var input = reader.ReadLine();
                    if (input != null)
                        // VertexCount: X
                        vcount = Convert.ToInt32(input.Split(new[] { ':' })[1].Trim());

                    input = reader.ReadLine();
                    if (input != null)
                        //TriangleCount: X
                        tcount = Convert.ToInt32(input.Split(new[] { ':' })[1].Trim());

                    // skip ahead to the vertex data
                    do {
                        input = reader.ReadLine();
                    } while (input != null && !input.StartsWith("{"));
                    // Get the vertices  
                    for (int i = 0; i < vcount; i++) {
                        input = reader.ReadLine();
                        if (input != null) {
                            var vals = input.Split(new[] { ' ' });
                            vertices.Add(
                                new Basic32(
                                    new Vector3(
                                        Convert.ToSingle(vals[0].Trim()),
                                        Convert.ToSingle(vals[1].Trim()),
                                        Convert.ToSingle(vals[2].Trim())),
                                    new Vector3(
                                        Convert.ToSingle(vals[3].Trim()),
                                        Convert.ToSingle(vals[4].Trim()),
                                        Convert.ToSingle(vals[5].Trim())),
                                        new Vector2()
                                )
                            );
                        }
                    }
                    // skip ahead to the index data
                    do {
                        input = reader.ReadLine();
                    } while (input != null && !input.StartsWith("{"));
                    // Get the indices
                    _skullIndexCount = 3 * tcount;
                    for (var i = 0; i < tcount; i++) {
                        input = reader.ReadLine();
                        if (input == null) {
                            break;
                        }
                        var m = input.Trim().Split(new[] { ' ' });
                        indices.Add(Convert.ToInt32(m[0].Trim()));
                        indices.Add(Convert.ToInt32(m[1].Trim()));
                        indices.Add(Convert.ToInt32(m[2].Trim()));
                    }
                }

                var vbd = new BufferDescription(VertexPN.Stride * vcount, ResourceUsage.Immutable,
                    BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                _skullVB = new Buffer(Device, new DataStream(vertices.ToArray(), false, false), vbd);

                var ibd = new BufferDescription(sizeof(int) * _skullIndexCount, ResourceUsage.Immutable,
                    BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                _skullIB = new Buffer(Device, new DataStream(indices.ToArray(), false, false), ibd);


            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }


    class Program {
        static void Main(string[] args) {
            Configuration.EnableObjectTracking = true;
            var app = new CameraDemo(Process.GetCurrentProcess().Handle);
            if (!app.Init()) {
                return;
            }
            app.Run();
        }
    }
}
