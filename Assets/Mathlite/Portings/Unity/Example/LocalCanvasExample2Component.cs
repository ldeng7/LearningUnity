using System.Collections.Generic;
using DM.Mathlite.Core.Models;

namespace DM.Mathlite.Portings.Unity.Example {
    public class LocalCanvasExample2Component: LocalCanvasComponent {
        protected override void Start() {
            base.Start();
            var itg = Symbol.Int;
            var rm = TextStyle.Mathrm;
            var d = ('d', rm);
            var view = this.r.CreateView(
                new GridModel(
                    new List<List<Model>>() {
                        new List<Model>() {
                            new object[] { itg, '0', d, 'x', },
                            '=',
                            'C',
                        },
                        new List<Model>() {
                            new object[] { itg, new ScriptModel('x', 'a', null), d, 'x', },
                            '=',
                            new object[] {
                                new DivisorModel('1', "a+1"), new ScriptModel('x', "a+1", null), "+C",
                                new SpaceModel(2), "(a", new OverlapModel('=', '/'), "-1)",
                            },
                        },
                        new List<Model>() {
                            new object[] { itg, new DivisorModel('1', 'x'), d, 'x', },
                            '=',
                            new object[] { ("ln", rm), "|x|+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, new ScriptModel('a', 'x', null), d, 'x', },
                            '=',
                            new object[] {
                                new DivisorModel(new ScriptModel('a', 'x', null), new object[] { ("ln", rm), 'a', }),
                                "+C", new SpaceModel(2), "(a>0,", new SpaceModel(1), 'a', new OverlapModel('=', '/'), "1)",
                            },
                        },
                        new List<Model>() {
                            new object[] { itg, new ScriptModel('e', 'x', null), d, 'x', },
                            '=',
                            new object[] { new ScriptModel('e', 'x', null), "+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, ("cos", rm), 'x', d, 'x', },
                            '=',
                            new object[] { "sin", "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, ("sin", rm), 'x', d, 'x', },
                            '=',
                            new object[] { '-', ("cos", rm), "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, new ScriptModel(new StringModel("sec", rm), '2', null), 'x', d, 'x', },
                            '=',
                            new object[] { ("tan", rm), "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, new ScriptModel(new StringModel("csc", rm), '2', null), 'x', d, 'x', },
                            '=',
                            new object[] { '-', ("cot", rm), "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, ("sec", rm), 'x', ("tan", rm), 'x', d, 'x', },
                            '=',
                            new object[] { ("sec", rm), "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, ("csc", rm), 'x', ("cot", rm), 'x', d, 'x', },
                            '=',
                            new object[] { '-', ("csc", rm), "x+C", },
                        },
                        new List<Model>() {
                            new object[] { itg, new DivisorModel('1', new ScriptModel("(1-x)", '2', null)), d, 'x', },
                            '=',
                            new object[] { ("arcsin", rm), "x+c", },
                        },
                        new List<Model>() {
                            new object[] {
                                itg, new DivisorModel('1', new object[] { "1+", new ScriptModel('x', '2', null) }),
                                d, 'x',
                            },
                            '=',
                            new object[] { ("arctan", rm), "x+c", },
                        },
                    },
                    new List<Alignment>() { Alignment.Right, Alignment.Center, Alignment.Left }
                ), 0.0002f);
            this.r.Render(view, -view.metrics.width / 2, -view.metrics.TotalHeight() / 2);
        }
    }
}
