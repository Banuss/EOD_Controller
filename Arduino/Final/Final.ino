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
  
  setDirectionMotor1(1);
  setDirectionMotor2(1);
  setSpeedMotor1(255);
  setSpeedMotor2(255);

  uint32_t currentFrequency;
  if (! ina219.begin()) {
    Serial.println("Failed to find INA219 chip 1");
    while (1) { delay(10); }
  }
  if (! ina219_2.begin()) {
    Serial.println("Failed to find INA219 chip 2");
    while (1) { delay(10); }
  }
  delay(100);
}

void loop() {
  delay(1);
  // Communication SEND DATA
  current_millis = millis();
  if (current_millis != prev_millis) {
    counter ++;
    if (counter > 1000) {
      counter = 0;
      if (Serial.availableForWrite() != 0) {
        Serial.println("--");
        int currentMotor1 = getCurrentMotor1();
        Serial.println(currentMotor1);
        int currentMotor2 = getCurrentMotor2();
        Serial.println(currentMotor2);
        if (currentMotor1 < 2000 && currentMotor2 < 2000) {
          if (currentMotor1 < 70) {
            currentMotor1 = 0;
          }
          else {
            currentMotor1 = map(currentMotor1,70, 1300, 1, 255);
          }
          
          if (currentMotor2 < 70) {
            currentMotor2 = 0;
          }
          else {
            currentMotor2 = map(currentMotor2,70, 1300, 1, 255);
          }
        
          unsigned int encoderMotor1 = getEncodervalueMotor1();
          unsigned int encoderMotor2 = getEncodervalueMotor2();
          
          Serial.println(encoderMotor1);
          Serial.println(encoderMotor2);
          Serial.println("--");

          bufSend[0] = char(currentMotor1);
          bufSend[1] = char(encoderMotor1 & 0xFF);
          bufSend[2] = char(encoderMotor1 >> 8);
          bufSend[3] = char(currentMotor2);
          bufSend[4] = char(encoderMotor2 & 0xFF);
          bufSend[5] = char(encoderMotor2 >> 8);
          //Serial.write(bufSend,6);
        }
        
        prev_millis = current_millis;
      }
    }
  }

  // Communication GET DATA
  getData();
  if (newData == true) {
    //uint8_t motorid = buf[0];
    auto motorid = bitRead(buf[0],0);
    auto dir = bitRead(buf[0],1);
    auto emer = bitRead(buf[0],2);
    auto speed = buf[1];
    Serial.println("id = " + String(motorid) + " dir: " + String(dir) + " Emergency: " + String(emer) + " speed: " + String(speed));
    if (motorid == 1) {
      setDirectionMotor1(dir);
      setSpeedMotor1(speed);
    }
    else {
      setDirectionMotor2(dir);
      setSpeedMotor2(speed);
    }
    // for (int i = 0; i++; i<8) {
    //   Serial.println(i);
    //   Serial.println(bitRead(buf[0], i));
    // }
    
    newData = false;
  }
}

void getData() {
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 2);
    Serial.println("here");
    //Serial.println(buf[0]);
    newData = true;
  }
}

