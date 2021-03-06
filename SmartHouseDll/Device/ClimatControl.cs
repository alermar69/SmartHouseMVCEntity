﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouseDll
{
    [Serializable]
    public class ClimatControl : Device, IClimatControl
    {
        private int _temperature;
        private int _humidity;
        private bool _auto;

        private bool _isInitAuto;
        private bool _isInitHumid;
        private bool _isInitTemper;

        public ClimatControl(string name)
            : base(name)
        {
            Init();
        }
        public ClimatControl()
        {            
        }
        private void Init()
        {
            Heat = new Heating("Отопление");
            Cond = new Conditioner("Кондиционер");
            Humid = new Humidifier("Увлажнитель");

            SensorHumid = new SensorHumidity("Датчик влажности");
            SensorHumid.ChangeHumidity += WatchHumidity;

            SensorTemperat = new SensorTemperature("Датчик температуры");
            SensorTemperat.ChangeTemperature += WatchTemperature;

            Auto = true;
            SetInitialTemperature();
            Humidity = 45;
        }

        #region Свойства
        public bool Auto
        {
            get { return _auto; }
            set
            {
                _auto = value;

                if (_isInitAuto)
                    OnOffAvto();
                else
                    _isInitAuto = true;
            }
        }
        public int Temperature
        {
            get { return _temperature; }
            set
            {
                if (value >= 5 && value <= 30)
                {
                    _temperature = value;
                    if (_isInitTemper)
                        WatchTemperature(SensorTemperat.CurrentTemperature);
                    else
                        _isInitTemper = true;
                }

            }
        }
        public int Humidity
        {
            get { return _humidity; }
            set
            {
                if (value >= 30 && value <= 60)
                {
                    _humidity = value;

                    if (_isInitHumid)
                        WatchHumidity(SensorHumid.CurrentHumidity);
                    else
                        _isInitHumid = true;

                }
            }
        }

        public virtual Heating Heat { get; set; }
        public virtual Conditioner Cond { get; set; }
        public virtual Humidifier Humid { get; set; }
        public virtual SensorTemperature SensorTemperat { get; set; }
        public virtual SensorHumidity SensorHumid { get; set; }



        #endregion


        public override void On()
        {
            base.On();
            OnOffAvto();
        }
        public override void Off()
        {
            base.Off();
            OffAllDevice();
        }

        #region MyRegion


        public void OnOffCond()
        {
            if (!Auto)
            {
                if (Cond.State == StateOnOff.Off)
                {
                    SetTemperDevice();
                    Cond.On();
                }
                else
                {
                    Cond.Off();
                }
            }
            else
            {
                throw new Exception("Включить кондиционер в автоматическом режиме климат-контроля невозможно");
            }
        }
        public void OnOffHumid()
        {
            if (!Auto)
            {
                if (Humid.State == StateOnOff.Off)
                {
                    Humid.On();
                }
                else
                {
                    Humid.Off();
                }
            }
            else
            {
                throw new Exception("Включить увлажнитель в автоматическом режиме климат-контроля невозможно");
            }
        }
        public void OnOffHeat()
        {
            if (!Auto)
            {
                if (Heat.State == StateOnOff.Off)
                {
                    SetTemperDevice();
                    Heat.On();
                }
                else
                {
                    Heat.Off();
                }
            }
            else
            {
                throw new Exception("Включить отопление в автоматическом режиме климат-контроля невозможно");
            }
        }

        private void OnOffAvto()
        {
            if (Auto)
            {
                SetTemperDevice();
                WatchHumidity(SensorHumid.CurrentHumidity);
                WatchTemperature(SensorTemperat.CurrentTemperature);
            }
            else
            {
                OffAllDevice();
            }
        }
        private void OffAllDevice()
        {
            Heat.Off();
            Humid.Off();
            Cond.Off();
        }
        private void WatchTemperature(int temper)
        {
            if (!Auto || State != StateOnOff.On) return;
            if (temper < Temperature)
            {
                Heat.On();
                Cond.Off();
            }
            if (temper > Temperature)
            {
                Heat.Off();
                Cond.On();
            }
            if (temper == Temperature)
            {
                Heat.Off();
                Cond.Off();
            }
        }
        private void WatchHumidity(int humidity)
        {
            if (!Auto || State != StateOnOff.On) return;
            if (humidity < Humidity)
            {
                Humid.On();
            }
            else
            {
                Humid.Off();
            }
        }

        #endregion

        public override string ToString()
        {
            string auto = "Ручной";
            if (Auto)
            {
                auto = "Авто";
            }

            string str = string.Format("{0}; Режим - {1}; t° воздуха - {2}; Влажность воздуха - {3}; ",
                base.ToString(), auto, SensorTemperat.CurrentTemperature, SensorHumid.CurrentHumidity);

            if (Auto)
            {
                str += String.Format("Требуемая t° - {0}; Требуемая влажность - {1}", Temperature, Humidity);
            }

            return str;
        }

        private void SetTemperDevice()
        {
            //if (Auto)
            //{
            //    Heat.SetInitialTemperature();
            //    Cond.SetInitialTemperature();                
            //}
            //else
            //{
            //    Heat.SetTemperature(Temperature);
            //    Cond.SetTemperature(Temperature);
            //}
        }

        #region Члены ITemperature
        public void IncrementTemperature()
        {
            Temperature++;
            SetTemperDevice();
        }

        public void DecrementTemperature()
        {
            Temperature--;
            SetTemperDevice();
        }

        public void SetInitialTemperature()
        {
            Temperature = 21;
        }

        public void SetTemperature(int temperature)
        {
            Temperature = temperature;
            SetTemperDevice();
        }
        #endregion
    }
}
