// Old varaibles for connumication
uint8_t buf[2]; //incoming
uint8_t bufSend[6]; //incoming

int newData;
long current_millis = 0;
long prev_millis = 0;
long counter = 0;

#include "motors.h"
#include "currentSensors.h"

#include <Wire.h>
#include <Adafruit_INA219.h>


// Defines
// Motor A
#define ENA 32
#define IN1 33
#define IN2 25
//Motor B
#define IN3 26
#define IN4 27
#define ENB 14

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
  
  setSpeedMotor1(255);
  setSpeedMotor2(255);

  uint32_t currentFrequency;
  if (! ina219.begin()) {
    Serial.println("Failed to find INA219 chip");
    while (1) { delay(10); }
  }
  delay(100);
}

void loop() {
  // Communication SEND DATA
  current_millis = millis();
  if (current_millis != prev_millis) {
    counter ++;
    if (counter > 1000) {
      counter = 0;
      if (Serial.availableForWrite() != 0) {
        // uint8_t currentMotor1 = getCurrentMotor1();
        // unsigned int encoderMotor1 = getEncoderMotor1();
        // uint8_t currentMotor2 = getCurrentMotor2();
        // unsigned int encoderMotor2 = getEncoderMotor2();
        uint8_t currentMotor1 = 104;
        unsigned int encoderMotor1 = 1024;
        uint8_t currentMotor2 = 105;
        unsigned int encoderMotor2 = 1025;

        bufSend[0] = currentMotor1;
        bufSend[1] = encoderMotor1 & 0xFF;
        bufSend[2] = encoderMotor1 >> 8;
        bufSend[3] = currentMotor2;
        bufSend[4] = encoderMotor2 & 0xFF;
        bufSend[5] = encoderMotor2 >> 8;
        Serial.write(bufSend,6);
        //Serial.println(current_mA);
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
    Serial.println("id = " + String(motorid) + " dir: " + String(dir) + " Emergency: " String(emer) + " speed: " + String(speed));
    
    // for (int i = 0; i++; i<8) {
    //   Serial.println(i);
    //   Serial.println(bitRead(buf[0], i));
    // }
    
    newData = false;
  }

  // Motor setings
  // Serial.println("on");
  // setDirectionMotor1(0);
  // setDirectionMotor2(0);
  // delay(2000);
  // Serial.println("switch");
  // setDirectionMotor1(0);
  // setDirectionMotor2(0);
  // delay(2000);
  // getCurrents();
}

void getData() {
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 2);
    Serial.println("here");
    //Serial.println(buf[0]);
    newData = true;
  }
}

