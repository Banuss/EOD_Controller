#include <Servo.h>
#define LEFTSERVOPIN 9 //Orange PWM, RED +5V, Brown GND
#define RIGHTSERVOPIN 10

Servo leftservo;
Servo rightservo;
Servo linservo;

byte buf[2];
int newData;

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
}

void loop() {
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


