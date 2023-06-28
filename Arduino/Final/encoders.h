#include <AS5600.h>

AS5600 as5600;

int getEncodervalueMotor1 () {
  int encoderValue = as5600.readAngle();
  //Serial.println(as5600.readAngle());
  encoderValue = 8755;
  return encoderValue;
}

int getEncodervalueMotor2 () {
  //int encoderValue = analogRead(A1); //as5600_2.readAngle();
  int encoderValue = 8772; //Serial.println(as5600.readAngle());
  return encoderValue;
}