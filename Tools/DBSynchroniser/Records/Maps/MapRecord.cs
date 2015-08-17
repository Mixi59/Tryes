﻿using System;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.Dlm;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.DofusProtocol.D2oClasses.Tools.Ele;

namespace DBSynchroniser.Records.Maps
{    
    public class MapRelator
    {
        public static string FetchQuery = "SELECT * FROM maps";
    }

    [TableName("maps")]
    public class MapRecord : IAutoGeneratedRecord, ISaveIntercepter
    {
        private short[] m_blueCells;
        private byte[] m_compressedCells;
        private byte[] m_compressedElements;
        private short[] m_redCells;

        public MapRecord()
        {
            
        }

        public MapRecord(DlmMap map)
        {
            Id = map.Id;
            
            Id = map.Id;
            Version = map.Version;
            RelativeId = map.RelativeId;
            MapType = map.MapType;
            SubAreaId = map.SubAreaId;
            ClientTopNeighbourId = map.TopNeighbourId;
            ClientBottomNeighbourId = map.BottomNeighbourId;
            ClientLeftNeighbourId = map.LeftNeighbourId;
            ClientRightNeighbourId = map.RightNeighbourId;
            ShadowBonusOnEntities = map.ShadowBonusOnEntities;
            UseLowpassFilter = map.UseLowPassFilter;
            UseReverb = map.UseReverb;
            PresetId = map.PresetId;
            Elements =
                map.Layers.SelectMany(
                    x =>
                        x.Cells.SelectMany(
                            y =>
                                y.Elements.OfType<DlmGraphicalElement>()
                                 .Where(z => z.Identifier != 0)
                                 .Select(z => new MapElement(z.Identifier, z.Cell.Id)))).ToArray();
            Cells =
                map.Cells.Select(
                    x =>
                        new Cell
                        {
                            Id = x.Id,
                            Floor = x.Floor,
                            LosMov = x.LosMov,
                            MapChangeData = x.MapChangeData,
                            MoveZone = x.MoveZone,
                            Speed = x.Speed
                        }).ToArray();
            bool any = Cells.Any(x => x.Walkable);
            BeforeSave(false);
        }

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///   Map version of this map.
        /// </summary>
        public uint Version
        {
            get;
            set;
        }

        /// <summary>
        ///   Relative id of this map.
        /// </summary>
        public uint RelativeId
        {
            get;
            set;
        }

        /// <summary>
        ///   Type of this map.
        /// </summary>
        public int MapType
        {
            get;
            set;
        }

        /// <summary>
        ///   Zone Id which owns this map.
        /// </summary>
        public int SubAreaId
        {
            get;
            set;
        }

        public int TopNeighbourId
        {
            get;
            set;
        }
        
        [DefaultSetting(-1)]
        public int TopNeighbourCellId
        {
            get;
            set;
        }

        public int BottomNeighbourId
        {
            get;
            set;
        }
        
        [DefaultSetting(-1)]
        public int BottomNeighbourCellId
        {
            get;
            set;
        }

        public int LeftNeighbourId
        {
            get;
            set;
        }

        [DefaultSetting(-1)]
        public int LeftNeighbourCellId
        {
            get;
            set;
        }

        public int RightNeighbourId
        {
            get;
            set;
        }
        
        [DefaultSetting(-1)]
        public int RightNeighbourCellId
        {
            get;
            set;
        }

        public int ClientTopNeighbourId
        {
            get;
            set;
        }

        public int ClientBottomNeighbourId
        {
            get;
            set;
        }

        public int ClientLeftNeighbourId
        {
            get;
            set;
        }

        public int ClientRightNeighbourId
        {
            get;
            set;
        }

        public int ShadowBonusOnEntities
        {
            get;
            set;
        }

        public bool UseLowpassFilter
        {
            get;
            set;
        }

        public bool UseReverb
        {
            get;
            set;
        }

        public int PresetId
        {
            get;
            set;
        }

        public byte[] BlueCellsBin
        {
            get;
            set;
        }

        public byte[] RedCellsBin
        {
            get;
            set;
        }

        [Ignore]
        public short[] BlueFightCells
        {
            get
            {
                return BlueCellsBin == null
                           ? new short[0]
                           : (m_blueCells ?? (m_blueCells = DeserializeFightCells(BlueCellsBin)));
            }
            set
            {
                m_blueCells = value;

                BlueCellsBin = value != null ? SerializeFightCells(value) : null;
            }
        }

        [Ignore]
        public short[] RedFightCells
        {
            get
            {
                return RedCellsBin == null
                           ? new short[0]
                           : (m_redCells ?? (m_redCells = DeserializeFightCells(RedCellsBin)));
            }
            set
            {
                m_redCells = value;
                RedCellsBin = value != null ? SerializeFightCells(value) : null;
            }
        }

        public byte[] CompressedCells
        {
            get { return m_compressedCells; }
            set
            {
                m_compressedCells = value;
                byte[] uncompressedCells = ZipHelper.Uncompress(m_compressedCells);

                Cells = new Cell[uncompressedCells.Length/Cell.StructSize];
                for (int i = 0, j = 0; i < uncompressedCells.Length; i += Cell.StructSize, j++)
                {
                    Cells[j] = new Cell();
                    Cells[j].Deserialize(uncompressedCells, i);
                }
            }
        }

        public byte[] CompressedElements
        {
            get { return m_compressedElements; }
            set
            {
                m_compressedElements = value;
                byte[] uncompressedElements = ZipHelper.Uncompress(m_compressedElements);

                Elements = new MapElement[uncompressedElements.Length/MapElement.Size];
                for (int i = 0, j = 0; i < uncompressedElements.Length; i += MapElement.Size, j++)
                {
                    var element = new MapElement();
                    element.Deserialize(uncompressedElements, i);

                    Elements[j] = element;
                }
            }
        }

        [Ignore]
        public MapElement[] Elements
        {
            get;
            set;
        }

        [Ignore]
        public Cell[] Cells
        {
            get;
            set;
        }

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            m_compressedCells = new byte[Cells.Length*Cell.StructSize];

            for (int i = 0; i < Cells.Length; i++)
            {
                Array.Copy(Cells[i].Serialize(), 0, m_compressedCells, i*Cell.StructSize, Cell.StructSize);
            }

            m_compressedCells = ZipHelper.Compress(m_compressedCells);

            m_compressedElements = new byte[Elements.Length*MapElement.Size];
            for (int i = 0; i < Elements.Length; i++)
            {
                Array.Copy(Elements[i].Serialize(), 0, m_compressedElements, i*MapElement.Size, MapElement.Size);
            }

            m_compressedElements = ZipHelper.Compress(m_compressedElements);
        }

        #endregion

        public static byte[] SerializeFightCells(short[] cells)
        {
            var bytes = new byte[cells.Length*2];

            for (int i = 0, l = 0; i < cells.Length; i++, l += 2)
            {
                bytes[l] = (byte) ((cells[i] & 0xFF00) >> 8);
                bytes[l + 1] = (byte) (cells[i] & 0xFF);
            }

            return bytes;
        }

        public static short[] DeserializeFightCells(byte[] bytes)
        {
            if ((bytes.Length%2) != 0)
                throw new ArgumentException("bytes.Length % 2 != 0");

            var cells = new short[bytes.Length/2];

            for (int i = 0, j = 0; i < bytes.Length; i += 2, j++)
                cells[j] = (short) (bytes[i] << 8 | bytes[i + 1]);

            return cells;
        }

        public MapElement[] FindMapElement(int id)
        {
            return Elements.Where(entry => entry.ElementId == id).ToArray();
        }

        public Stump.Server.WorldServer.Database.World.Maps.MapRecord GetWorldRecord()
        {
            var record = new Stump.Server.WorldServer.Database.World.Maps.MapRecord
            {
                Id = Id,
                Version = Version,
                RelativeId = RelativeId,
                MapType = MapType,
                SubAreaId = SubAreaId,
                TopNeighbourId = TopNeighbourId,
                TopNeighbourCellId = TopNeighbourCellId,
                BottomNeighbourId = BottomNeighbourId,
                BottomNeighbourCellId = BottomNeighbourCellId,
                LeftNeighbourId = LeftNeighbourId,
                LeftNeighbourCellId = LeftNeighbourCellId,
                RightNeighbourId = RightNeighbourId,
                RightNeighbourCellId = RightNeighbourCellId,
                ClientTopNeighbourId = ClientTopNeighbourId,
                ClientBottomNeighbourId = ClientBottomNeighbourId,
                ClientLeftNeighbourId = ClientLeftNeighbourId,
                ClientRightNeighbourId = ClientRightNeighbourId,
                ShadowBonusOnEntities = ShadowBonusOnEntities,
                UseLowpassFilter = UseLowpassFilter,
                UseReverb = UseReverb,
                PresetId = PresetId,
                BlueFightCells = BlueFightCells,
                RedFightCells = RedFightCells,
                CompressedCells = CompressedCells,
                CompressedElements = CompressedElements
            };

            return record;
        }
    }
}