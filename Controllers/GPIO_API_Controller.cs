using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Device.Gpio;
namespace AquaLamp.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPIO_API_Controller : ControllerBase
    {
        [HttpPost]
        [HttpGet]
        [Route("/api/LEDMATRIX/HEART")]
        public void DoHeart()
        {
            LED_Matrix.Heart();
        }
        [HttpPost]
        [HttpGet]
        [Route("/api/LEDMATRIX/CIRCLE")]
        public void DoCircle()
        {
            LED_Matrix.Circle();
        }
    }

    class LED_Matrix
    {
        static int I2C_IDLE = 1;
        public static void Heart()
        {
            int scl = 2;
            int sda = 3;

            
            GpioController controller = new GpioController();

            controller.OpenPin(scl, PinMode.Output);
            controller.OpenPin(sda, PinMode.Output);
            //controller.Read(scl);
            //controller.OpenPin(sda, PinMode.Input);


            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b01000000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b10001000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b11000000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b01000000);
            Send(controller, scl, sda, 0b00100100);
            Send(controller, scl, sda, 0b01111110);
            Send(controller, scl, sda, 0b11111111);
            Send(controller, scl, sda, 0b11111111);
            Send(controller, scl, sda, 0b01111110);
            Send(controller, scl, sda, 0b00111100);
            Send(controller, scl, sda, 0b00011000);
            Send(controller, scl, sda, 0b00000000);

            End(controller, scl, sda);
            Console.Read();
        }
        public static void Circle()
        {
            int scl = 2;
            int sda = 3;


            GpioController controller = new GpioController();

            controller.OpenPin(scl, PinMode.Output);
            controller.OpenPin(sda, PinMode.Output);
            //controller.Read(scl);
            //controller.OpenPin(sda, PinMode.Input);


            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b01000000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b10001000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b11000000);
            End(controller, scl, sda);
            ////
            Start(controller, scl, sda);
            Send(controller, scl, sda, 0b01000000);
            Send(controller, scl, sda, 0b00111100);
            Send(controller, scl, sda, 0b01111110);
            Send(controller, scl, sda, 0b11111111);
            Send(controller, scl, sda, 0b11111111);
            Send(controller, scl, sda, 0b01111110);
            Send(controller, scl, sda, 0b00111100);
            Send(controller, scl, sda, 0b00000000);
            Send(controller, scl, sda, 0b00000000);

            End(controller, scl, sda);
            Console.Read();
        }
        static void Start(GpioController gpio, int scl, int sda)
        {
            //gpio.Write(scl, PinValue.Low);
            //System.Threading.Thread.Sleep(I2C_IDLE);
            //gpio.Write(sda, PinValue.Low);
            //System.Threading.Thread.Sleep(I2C_IDLE);

            gpio.Write(scl, PinValue.High);
            gpio.Write(sda, PinValue.High);
            System.Threading.Thread.Sleep(I2C_IDLE);
            gpio.Write(sda, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);

            gpio.Write(scl, PinValue.Low);
            gpio.Write(sda, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);
        }
        static void End(GpioController gpio, int scl, int sda)
        {
            gpio.Write(scl, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);
            gpio.Write(sda, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);

            gpio.Write(scl, PinValue.High);
            //gpio.Write(sda, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);
            gpio.Write(sda, PinValue.High);
            System.Threading.Thread.Sleep(I2C_IDLE);

            //gpio.Write(scl, PinValue.Low);
            //gpio.Write(sda, PinValue.Low);
            System.Threading.Thread.Sleep(I2C_IDLE);
        }
        static void Send(GpioController gpio, int scl, int sda, byte data)
        {


            int i = 0;
            byte temp = data;
            for (i = 0; i < 8; i++)
            {
                if ((temp & 0x01) == 0)
                {
                    gpio.Write(scl, PinValue.Low);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(sda, PinValue.Low);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(scl, PinValue.High);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(scl, PinValue.Low);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                }
                else
                {
                    gpio.Write(scl, PinValue.Low);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(sda, PinValue.High);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(scl, PinValue.High);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                    gpio.Write(scl, PinValue.Low);
                    System.Threading.Thread.Sleep(I2C_IDLE);
                }
                temp = (byte)(temp / 2);
            }

        }
    }

}