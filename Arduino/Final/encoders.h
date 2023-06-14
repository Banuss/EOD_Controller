#include <AS5600.h>

AS5600 as5600;

int getEncodervalueMotor1 () {
  int encoderValue = as5600.readAngle();
  //Serial.println(as5600.readAngle());
  return encoderValue;
}