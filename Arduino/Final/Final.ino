#include <Servo.h>
#include <Adafruit_INA219.h>



#define LEFTSERVOPIN 9 //Orange PWM, RED +5V, Brown GND
#define RIGHTSERVOPIN 10
#define LINSERVOPIN 11

Servo leftservo;
Servo rightservo;
Servo linservo;

Adafruit_INA219 ina219;

byte buf[2];
int newData;

long current_millis = 0;
long prev_millis = 0;
int counter = 0;



void getData() {
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 3);
    newData = true;
  }
}

void setup() {
  Serial.begin(115200);       
  leftservo.attach(LEFTSERVOPIN);
  rightservo.attach(RIGHTSERVOPIN);
  linservo.attach(LINSERVOPIN);
  ina219.begin();
  uint32_t currentFrequency;
  delay(100);
}

void loop() {
  // current_millis = millis();
  // if (current_millis != prev_millis) {
  //   counter ++;
  //   if (counter > 100) counter = 0;{
  //     if (Serial.available() != 0) {
  //       float current_mA = 0;
  //       current_mA = ina219.getCurrent_mA();
  //       //Serial.println(current_mA);
  //       prev_millis = current_millis;
  //     }
  //   }
  // }

  getData();
  if (newData == true) {
    if ((int)buf[0] == 0)
    {
      linservo.write((int)buf[1]);
    }
    if ((int)buf[0]== 1)
    {
      leftservo.write((int)buf[1]);
    }
    if ((int)buf[0] == 2)
    {
      rightservo.write((int)buf[1]);
    }
    newData = false;
  }
}


