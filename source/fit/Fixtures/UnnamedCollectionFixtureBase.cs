// Copyright � 2009 Syterra Software Inc.
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License version 2.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

using System.Collections;
using fit;
using fitSharp.Fit.Model;
using fitSharp.Machine.Engine;
using fitSharp.Machine.Model;

namespace fitlibrary {

    public class UnnamedCollectionFixtureBase: CollectionFixtureBase {

        public UnnamedCollectionFixtureBase(IEnumerator theEnumerator): base(theEnumerator) {}

        public UnnamedCollectionFixtureBase(object[][] theActualValues): base(theActualValues) {}

        public override void DoTable(Parse theTable) {
            CompareRows(theTable.Parts, theTable.Parts.More);
        }

        protected override ListMatchStrategy MatchStrategy {
            get {return new UnnamedCollectionMatchStrategy();}
        }

        private class UnnamedCollectionMatchStrategy: ListMatchStrategy {
            public bool IsOrdered {get { return true; }}
            public bool SurplusAllowed {get {return false;}}
            public TypedValue[] ActualValues(Processor<Cell> processor, object theActualRow) {
                var actuals = (object[]) theActualRow;
                var result = new TypedValue[actuals.Length];
                for (int i = 0; i < actuals.Length; i++) result[i] = new TypedValue(actuals[i]);
                return result;
            }
            public bool IsExpectedSize(Parse theExpectedCells, object theActualRow) {
                return (theExpectedCells.Size == ((object[])theActualRow).Length);
            }
            public bool FinalCheck(Fixture fixture) {return true;}
        }
    }
}