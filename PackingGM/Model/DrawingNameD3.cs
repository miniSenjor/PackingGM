using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Связь чертежного обозначения и Д3
    /// </summary>
    public class DrawingNameD3 : BaseModel
    {
        private int _drawingNameVersionId;
        public int DrawingNameVersionId
        {
            get => _drawingNameVersionId;
            set
            {
                _drawingNameVersionId = value;
                OnPropertyChanged(nameof(DrawingNameVersionId));
            }
        }
        private int _d3Id;
        public int D3Id
        {
            get => _d3Id;
            set
            {
                _d3Id = value;
                OnPropertyChanged(nameof(D3Id));
            }
        }
        public DrawingNameVersion DrawingNameVersion { get; set; }
        public D3 D3 { get; set; }
    }
}
