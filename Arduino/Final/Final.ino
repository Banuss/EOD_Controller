uint8_t buf[2]; //incoming
uint8_t bufSend[8]; //incoming

int newData;
long current_millis = 0;

long prev_millis = 0;
long prev_millis_conn = 0;

long counter = 0;
long counter_conn =0;

#include "motors.h"
#include "currentSensors.h"
#include "encoders.h"

#include <Wire.h>
#include <Adafruit_INA219.h>

// Defines
// Motor A
#define ENA 5
#define IN1 6
#define IN2 7
//Motor B
#define IN3 8
#define IN4 9
#define ENB 10


#define MAXCURRENT 1200
// Others
void setup() {
  Serial.begin(115200);   
  
  // Set pins
  pinMode(ENA, OUTPUT);
	pinMode(ENB, OUTPUT);
	pinMode(IN1, OUTPUT);
	pinMode(IN2, OUTPUT);
	pinMode(IN3, OUTPUT);
	pinMode(IN4, OUTPUT);
	
	// Turn off motors - Initial state
	digitalWrite(IN1, LOW);
	digitalWrite(IN2, LOW);
	digitalWrite(IN3, LOW);
	digitalWrite(IN4, LOW);

  uint32_t currentFrequency;

  if (! ina219_2.begin()) {
    Serial.println("Failed to find INA219 chip");
    while (1) { delay(10); }
  }
  if (! ina219.begin()) {
    Serial.println("Failed to find INA219 chip");
    while (1) { delay(10); }
  }
  delay(50);
}

void loop() {
  delay(1);
  //Communication SEND DATA
  current_millis = millis();

  if (current_millis != prev_millis) {
    counter ++;
    if (counter > 20) {
      counter = 0;
     
      if (Serial.availableForWrite() != 0) {     
        
        int currentMotor1 = getCurrentMotor1();
        int currentMotor2 = getCurrentMotor2();

        if (currentMotor1 <= MAXCURRENT && currentMotor2 <= MAXCURRENT) {
          
          if (currentMotor1 < 70) {
            currentMotor1 = 0;
          }
          else {
            currentMotor1 = map(currentMotor1,70, MAXCURRENT, 1, 255);
          }

          if (currentMotor2 < 70) {
            currentMotor2 = 0;
          }
          else {
            currentMotor2 = map(currentMotor2,70, MAXCURRENT, 1, 255);
          }

          unsigned int encoderMotor1 = getEncodervalueMotor1();
          unsigned int encoderMotor2 = getEncodervalueMotor2();

          bufSend[0] = char(currentMotor1);
          bufSend[1] = char(encoderMotor1 & 0xFF);
          bufSend[2] = char(encoderMotor1 >> 8);
          bufSend[3] = char(currentMotor2);
          bufSend[4] = char(encoderMotor2 & 0xFF);
          bufSend[5] = char(encoderMotor2 >> 8);
          bufSend[6] = char(128);
          bufSend[7] = char(128);
          Serial.write(bufSend,8);
          //memset(bufSend, 0, 6);
        }
        else if (currentMotor1 > MAXCURRENT || currentMotor2 > MAXCURRENT)
        {
            setDirectionMotor1(0);
            setDirectionMotor2(0);
            setSpeedMotor1(0);
            setSpeedMotor2(0);
            return 0;
        }
        prev_millis = current_millis;
      }
    }
  }

  //Save the plastics! Ga uit wanneer connectie wegvalt
  if (current_millis - prev_millis_conn > 500) 
  { 
    prev_millis_conn = current_millis;
    setDirectionMotor1(0);
    setDirectionMotor2(0);
    setSpeedMotor1(0);
    setSpeedMotor2(0);
  }




  //RECEIVE DATA FROM GUI (MOTOR SPEED)
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 2);
    newData = true;
    prev_millis_conn = current_millis;
  }

  if (newData == true) {
    bool motorid = bitRead(buf[0],0);
    bool dir = bitRead(buf[0],1);
    bool emer = bitRead(buf[0],2);
    auto speed = buf[1];

    if (!motorid && !emer) {
      setDirectionMotor1((dir)?1:2);
      setSpeedMotor1(speed);
    }
    else if (motorid && !emer){
      setDirectionMotor2((dir)?1:2);
      setSpeedMotor2(speed);
    }
    if(emer){
      setDirectionMotor1(0);
      setDirectionMotor2(0);
      setSpeedMotor1(0);
      setSpeedMotor2(0);
    }
    newData = false;
  }


}

