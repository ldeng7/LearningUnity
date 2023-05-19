using System.Collections.Generic;
using DM.Mathlite.Core.Models;

namespace DM.Mathlite.Portings.Unity.Example {
    public class LocalCanvasExample1Component: LocalCanvasComponent {
        protected override void Start() {
            base.Start();
            var rm = TextStyle.Mathrm;
            var d = ('d', rm);
            var view = this.r.CreateView(
                new GridModel(
                    new List<List<Model>>() {
                        new List<Model>() {
                            new object[] { d, 'C', },
                            '=',
                            '0',
                        },
                        new List<Model>() {
                            new object[] { d, '(', new ScriptModel('x', 'a', null), ')', },
                            '=',
                            new object[] { 'a', new ScriptModel('x', "a-1", null), d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { d, '(', new ScriptModel('a', 'x', null), ')', },
                            '=',
                            new object[] { new ScriptModel('a', 'x', null), ("ln", rm), 'a', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { d, '(', new ScriptModel('e', 'x', null), ')', },
                            '=',
                            new object[] { new ScriptModel('e', 'x', null), d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { d, '(', new ScriptModel(new StringModel("log", rm), null, 'a'), "|x|)", },
                            '=',
                            new object[] { new DivisorModel('1', new object[] { 'x', ("ln", rm), 'a', }), d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { d, '(', ("ln", rm), "|x|)", },
                            '=',
                            new object[] { new DivisorModel('1', 'x'), d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("sin", rm), "x)", },
                            '=',
                            new object[] { ("cos", rm), 'x', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("cos", rm), "x)", },
                            '=',
                            new object[] { '-', ("sin", rm), 'x', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("tan", rm), "x)", },
                            '=',
                            new object[] {  new ScriptModel(new StringModel("sec", rm), '2', null), 'x', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("cot", rm), "x)", },
                            '=',
                            new object[] {
                                '-', new ScriptModel(new StringModel("csc", rm), '2', null), 'x', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("sec", rm), "x)", },
                            '=',
                            new object[] { ("sec", rm), 'x', ("tan", rm), 'x', d, 'x', },
                        },
                        new List<Model>() {
                            new object[] { ('d', rm), '(', ("csc", rm), "x)", },
                            '=',
                            new object[] { '-', ("csc", rm), 'x', ("cot", rm), 'x', d, 'x', },
                        },
                    },
                    new List<Alignment>() { Alignment.Right, Alignment.Center, Alignment.Left }
                ), 0.0002f);
            this.r.Render(view, -view.metrics.width / 2, -view.metrics.TotalHeight() / 2);
        }
    }
}
