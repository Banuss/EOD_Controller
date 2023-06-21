// Old varaibles for connumication
uint8_t buf[2]; //incoming
uint8_t bufSend[6]; //incoming

int newData;
long current_millis = 0;
long prev_millis = 0;
long counter = 0;

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

// Encoders
#define SCL 12
#define SDA 13

// Current sensors
#define CS1 34
#define CS2 35

// Variables

// Others

void setup() {
  Serial.begin(115200);  
  memset(bufSend, 0, 6);     
  
  // Old code for communication
  // ina219.begin();
  // uint32_t currentFrequency;

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
  if (! ina219.begin()) {
    //Serial.println("Failed to find INA219 chip 1");
    while (1) { delay(10); }
  }
  if (! ina219_2.begin()) {
    //Serial.println("Failed to find INA219 chip 2");
    while (1) { delay(10); }
  }
  delay(100);
}

void loop() {
  // Communication SEND DATA
  current_millis = millis();
  if (current_millis != prev_millis) {
    counter ++;
    if (counter > 30) {
      counter = 0;
      if (Serial.availableForWrite() != 0) {     
        
        char sendcurrent1 = map(getCurrentMotor1(),70, 1300, 1, 255);
        char sendcurrent2 = map(getCurrentMotor2(),70, 1300, 1, 255);
        
        int encoderMotor1 = getEncodervalueMotor1();
        int encoderMotor2 = getEncodervalueMotor2();
       
        bufSend[0] = char(sendcurrent1);
        bufSend[1] = char(encoderMotor1 << 8);
        bufSend[2] = char(encoderMotor1);
        bufSend[3] = char(sendcurrent2);
        bufSend[4] = char(encoderMotor2 << 8);
        bufSend[5] = char(encoderMotor2);
        Serial.write(bufSend,6);
        memset(bufSend, 0, 6);
      }
      prev_millis = current_millis;
    }
  }
  

  //RECEIVE DATA FROM GUI (MOTOR SPEED)
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 2);
    newData = true;
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

