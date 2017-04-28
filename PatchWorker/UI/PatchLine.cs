﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PatchWorker.Graph;
using System.Xml.Serialization;

namespace PatchWorker.UI
{
    [Serializable]
    public class PatchLine
    {
        [XmlIgnore]
        public PatchCanvas canvas;
        [XmlIgnore]
        public PatchBox outBox;
        [XmlIgnore]
        public Point outEnd;
        [XmlIgnore]
        public PatchBox inBox;
        [XmlIgnore]
        public Point inEnd;
        [XmlIgnore]
        public PatchCord patchcord;
        public int inNum;               //num of connecting boxes for rebuilding patch upon de-serialization
        public int outNum;

        readonly Pen CONNECTORCOLOR = new Pen(Color.Red, 2.0f);

        //new line starts at first box's output jack, the input end follows the mouse until it is dropped on a target box
        public PatchLine(PatchCanvas _canvas, PatchBox _outbox, Point _inEnd)
        {
            canvas = _canvas;
            patchcord = null;           //not connected to input jack yet     

            connectOutputJack(_outbox);

            inBox = null;
            inEnd = _inEnd;
            inNum = 0;
        }

        //no param cons for serialization
        public PatchLine()
        {
            canvas = null;
            patchcord = null;

            outBox = null;
            outEnd = new Point(0, 0);
            outNum = 0;

            inBox = null;
            inEnd = new Point(0, 0);
            inNum = 0;
        }

        public void connectOutputJack(PatchBox _outbox)
        {
            outBox = _outbox;
            outEnd = outBox.outJackPos();
            outNum = outBox.boxNum; 
            outBox.connectOutJack(this);            
        }

        public void connectInputJack(PatchBox _inbox)
        {
            inBox = _inbox;
            inEnd = inBox.inJackPos(); 
            inNum = inBox.boxNum;
            inBox.connectInJack(this);                                      //connect line & box in view
            patchcord = outBox.unit.connectOutUnit(inBox.unit);             //connect units in model         
        }

        public void disconnect()
        {
            if (inBox != null) inBox.disconnectInJack(this);
            inBox = null;
            inNum = 0;
            if (outBox != null) outBox.disconnectOutJack(this);
            outBox = null;
            outNum = 0;
            if (patchcord != null) patchcord.disconnect();      //patchcord only set if both ends were connected
        }

        public void setInEndPos(Point _inEnd)
        {
            inEnd = _inEnd;
        }

        public void setOutEndPos(Point _outEnd)
        {
            outEnd = _outEnd;
        }

        public void paint(Graphics g)
        {
            g.DrawLine(CONNECTORCOLOR, outEnd, inEnd);    
        }
    }
}