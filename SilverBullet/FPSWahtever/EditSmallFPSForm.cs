using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace MknGames.FPSWahtever
{
    //abstract class ScriptObject
    //{

    //}
    public partial class EditSmallFPSForm : Form
    {
        public class Node
        {
            //double link list
            public Node child;
            public Node parent;

            //data
            public object data;
            public string fieldName = "";
            public MemberInfo[] members;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="parent">can be null</param>
            /// <param name="info">cant be null</param>
            public Node(Node parent, object info, string name)
            {
                this.parent = parent;
                child = null;
                data = info;
                fieldName = name;
                RefreshFields();
            }

            public void RefreshFields()
            {
                members = data.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }

        SmallFPS fps;
        MemberInfo currentMember;
        Node root;
        Node currentNode;
        Node currentFieldParent;
        Action setFieldAction = null;
        BindingSource bindingSource;

        public bool SetReady()
        {
            return setFieldAction != null;
        }
        public void PerformSet()
        {
            setFieldAction();
            setFieldAction = null;
        }

        public EditSmallFPSForm(SmallFPS hostFps)
        {
            InitializeComponent();
            this.fps = hostFps;
            root = new Node(null, hostFps, "root");
            SetNode(root);
            for (int i = 0; i < 10; ++i)
            {
                TextBox box = new TextBox();
                box.MinimumSize = new Size(100, 20);
                box.Parent = argumentFlowPanel;
            }
            //dataGridView1.DataSource = null;// table;
            
            //argumentFlowPanel
            //box.Location = 
            //argumentListBox.Items.Add()
        }

        // select member
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemberInfo f = (MemberInfo)(listBox1.SelectedItem);
            SetCurrentMemberInfo(f);
        }

        void SetCurrentMemberInfo(MemberInfo info)
        {
            currentFieldParent = currentNode;
            currentMember = info;
            RefreshCurrentMemberInfo();
        }
        void RefreshCurrentMemberInfo()
        {
            if (currentMember == null)
            {
                label2.Text = "";
                textBox1.Text = "";
                listBox2.Items.Clear();
                return;
            }
            //clear
            label2.Text = currentMember == null ? "" : currentMember.Name;
            argumentFlowPanel.Controls.Clear();
            executeButton.Enabled = false;

            //field or property
            object value = null;
            MethodInfo getValue = currentMember.GetType().GetMethod("GetValue", new Type[] { typeof(object) });
            if (getValue != null)
            {
                value = getValue.Invoke(currentMember, new object[] { currentFieldParent.data });
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
            textBox1.Text = value == null ? "null" : value.ToString();

            //collection
            IEnumerable array = value as IEnumerable;
            listBox2.Items.Clear();
            if (array != null)
            {
                foreach (var a in array)
                {
                    listBox2.Items.Add(a);
                }
            }

            //method
            if(currentMember.MemberType == MemberTypes.Method)
            {
                executeButton.Enabled = true;
                MethodInfo method = currentMember as MethodInfo;
                var parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; ++i)
                {
                    ParameterInfo parameter = parameters[i];
                    FlowLayoutPanel container = new FlowLayoutPanel();
                    container.AutoSize = true;
                    container.Parent = argumentFlowPanel;
                    Label label = new Label();
                    label.Text = parameter.Name;
                    label.Parent = container;
                    TextBox textbox = new TextBox();
                    textbox.MinimumSize = new Size(100, 20);
                    textbox.Parent = container;
                }
            }
        }


        //double click, set current node
        void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            SetNodeFromMemberInfo((MemberInfo)listBox1.SelectedItem);
        }
        public void SetNodeFromMemberInfo(MemberInfo member)
        {
            MethodInfo getValue = member.GetType().GetMethod("GetValue", new Type[] { typeof(object) });
            //FieldInfo next = (FieldInfo)(listBox1.SelectedItem);
            if (getValue == null)
                return;
            object data = getValue.Invoke(member, new object[] { currentNode.data });
            if (data == null)
                return;
            Node newNode = new Node(currentNode,
                data,
                member.Name);
            SetNode(newNode);
        }
        public bool GetValueFromMemberInfo(MemberInfo member, out object value)
        {
            value = null;
            MethodInfo getValue = member.GetType().GetMethod("GetValue", new Type[] { typeof(object) });
            //FieldInfo next = (FieldInfo)(listBox1.SelectedItem);
            if (getValue == null)
                return false;
            value = getValue.Invoke(member, new object[] { currentNode.data });
            return true;
        }
        void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                SetNode(new Node(currentNode,
                    listBox2.SelectedItem,
                    listBox2.SelectedItem.ToString()));
            }
        }

        //set node
        public void SetNode(Node n)
        {
            if (currentNode == n)
                System.Diagnostics.Debugger.Break();
            searchTextBox.Text = "";
            if(currentNode != null)
                currentNode.child = n;
            currentNode = n;
            RefreshCurrentNodeGUI();
        }
        public void RefreshCurrentNodeGUI()
        {
            label1.Text = currentNode.fieldName;
            button1.Enabled = currentNode.parent != null;
            button2.Enabled = currentNode != root;
            listBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            string searchText = searchTextBox.Text.ToLower();
            for (int i = 0; i < currentNode.members.Length; ++i)
            {
                MemberInfo a = currentNode.members[i];
                string fieldText = a.Name.ToLower();
                if (fieldText.Contains(searchText) == true)
                {
                    listBox1.Items.Add(a);
                    object value;
                    GetValueFromMemberInfo(a, out value);
                    dataGridView1.Rows.Add(a.Name, a.MemberType, a, value);
                    if (a == currentMember)
                    {
                        listBox1.SelectedItem = a;
                    }
                }
            }
            listBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetNode(root);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = currentNode.fieldName;
            SetNode(currentNode.parent);
            searchTextBox.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (setFieldAction != null)
                richTextBox1.Text = "Previous operation not yet processed by game.";
            object expressionResult = null;
            var results = CompileExpression(textBox1.Text, out expressionResult);
            if (results.Errors.Count == 0)
            {
                setFieldAction = () =>
                {
                    MethodInfo setValue = currentMember.GetType().GetMethod("SetValue", new Type[] { typeof(object), typeof(object) });
                    if (setValue != null)
                    {
                        if (expressionResult is double)//i cant be bothered to write f
                        {
                            double value = (double)expressionResult;
                            expressionResult = (float)value;
                        }
                        setValue.Invoke(currentMember, new object[] { currentFieldParent.data, expressionResult });
                    }
                };
            }
        }

        public CompilerResults CompileExpression(string expression,  out object expressionResult)
        {
            //https://stackoverflow.com/questions/10314815/trying-to-compile-and-execute-c-sharp-code-programmatically
            CSharpCodeProvider provider = new CSharpCodeProvider();

            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.TreatWarningsAsErrors = false;
            parameters.CompilerOptions = "/optimize";

            var assembly = fps.GetType().Assembly;
            parameters.ReferencedAssemblies.Add(assembly.ManifestModule.Name);
            var assemblies = assembly.GetReferencedAssemblies();
            List<Assembly> referencedAssemblies = new List<Assembly>();
            referencedAssemblies.Add(assembly);
            foreach (AssemblyName a in assemblies)
            {
                Assembly refAssembly = Assembly.ReflectionOnlyLoad(a.FullName);
                parameters.ReferencedAssemblies.Add(refAssembly.ManifestModule.Name);
                referencedAssemblies.Add(refAssembly);
            }
            string compiledTypeName = "SCRIPTING_OBJECT_IN_MEMORY";
            string compiledMethodName = "LAMBDA";
            // Expression<editField.FieldType>
            string code = string.Format(@"
using MknGames._2D;
using MknGames.Split_Screen_Dungeon;
using MknGames.Split_Screen_Dungeon.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using NAudio;
using NAudio.Wave;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;
using System.Reflection;

class {0}
{{
    public static object {1}()
    {{
        return {2};
    }}
}}
",
compiledTypeName,
compiledMethodName,
expression);
            CompilerResults compile = provider.CompileAssemblyFromSource(parameters, code);
            //richTextBox1.Text = "";
            foreach (var msg in compile.Errors)
            {
                richTextBox1.Text += msg.ToString() + '\n';
            }
            expressionResult = null;
            if (compile.Errors.Count == 0)
            {
                Type dummyType = compile.CompiledAssembly.GetType(compiledTypeName);
                MethodInfo method = dummyType.GetMethod(compiledMethodName);
                expressionResult = method.Invoke(null, null);
            }
            Console.WriteLine(expression + "=>" + expressionResult);
            return compile;
        }

        //refresh button click
        private void button3_Click_1(object sender, EventArgs e)
        {
            Node start = root;
            while (start != null)
            {
                start.RefreshFields();
                start = start.child;
            }
            RefreshCurrentNodeGUI();
            RefreshCurrentMemberInfo();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            fps.saveRequested = true;
        }

        //search
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            RefreshCurrentNodeGUI();
        }

        //execute method
        private void button4_Click(object sender, EventArgs e)
        {
            MethodInfo method = currentMember as MethodInfo;
            var parameters = method.GetParameters();
            object[] parameterValues = new object[parameters.Length];
            bool errorsOccured = false;
            for(int i = 0; i < parameters.Length;++i)
            {
                object value = null;
                string expression = argumentFlowPanel.Controls[i].Controls[1].Text;
                var results = CompileExpression(expression, out value);
                if (results.Errors.Count != 0)
                    errorsOccured = true;
                if (value is double)
                    value = (float)(double)value;
                parameterValues[i] = value;
            }
            if (errorsOccured)
                return;
            method.Invoke(currentNode.data, parameterValues);
        }

        private void cubes_SubdivideButton_Click(object sender, EventArgs e)
        {
            fps.editBoxesRequestSubdivide = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var row = dataGridView1.Rows[e.RowIndex];
            SetCurrentMemberInfo((MemberInfo)row.Cells[2].Value);
        }
        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            SetNodeFromMemberInfo((MemberInfo)row.Cells[2].Value);
        }

        //reload shader
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    Console.WriteLine("HLSL 2 MGFX begin.");
        //    Console.WriteLine("Open...");
        //    string fileIn = shaderInTextBox.Text;
        //    string fileOut = shaderOutTextBox.Text;
        //    Console.WriteLine(fileOut);
        //    string profile = "/Profile:DirectX_11";

        //    Console.WriteLine("2MGFX.exe launched...");
        //    {
        //        string arguments = fileIn + " " + fileOut + ' ' + profile;
        //        Process cmd = new Process();
        //        cmd.StartInfo.FileName = "C:/Program Files (x86)/MSBuild/MonoGame/v3.0/Tools/2MGFX.exe";
        //        cmd.StartInfo.Arguments = arguments;
        //        //cmd.StartInfo.RedirectStandardInput = true;
        //        cmd.StartInfo.RedirectStandardOutput = true;
        //        cmd.StartInfo.CreateNoWindow = false;
        //        cmd.StartInfo.UseShellExecute = false;
        //        cmd.Start();
        //        richTextBox1.Text = "";
        //        while (!cmd.StandardOutput.EndOfStream)
        //        {
        //            string line = cmd.StandardOutput.ReadLine();
        //            // do something with line
        //            richTextBox1.Text += line;
        //        }
        //    }
        //    Console.WriteLine("2MGFX.exe exited...");

        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(File.OpenRead(fileOut)))
        //        {
        //            string text = reader.ReadToEnd();
        //            byte[] data = Encoding.ASCII.GetBytes(text);
        //            Microsoft.Xna.Framework.Graphics.Effect effect = new Microsoft.Xna.Framework.Graphics.Effect(fps.GraphicsDevice, data);
        //            Debugger.Break();
        //            effect.Dispose();
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        Debugger.Break();
        //        richTextBox1.Text += '\n' + exc.Message;
        //    }
        //}
    }
}
