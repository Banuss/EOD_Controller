//Motor B
#define ENA 5
#define IN1 6
#define IN2 7
//Motor B
#define IN3 8
#define IN4 9
#define ENB 10

// Control speed of the motors
// Speed in range 0-255, 0 = SLOW, 255 = MAX SPEED
void setSpeedMotor1 (int speed) {
  analogWrite(ENA, speed);
}

void setSpeedMotor2 (int speed) {
  analogWrite(ENB, speed);
}

// Control spinning direction of motors
// 0 = OFF, 1 = LEFT, 2 = RIGHT
int setDirectionMotor1 (int dir) {
  if (dir == 0) {
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, LOW);
  }
  else if (dir == 1) {
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
  }
  else {
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
  }
}

int setDirectionMotor2 (int dir) {
  if (dir == 0) {
    digitalWrite(IN3, LOW);
    digitalWrite(IN4, LOW);
  }
  else if (dir == 1) {
    digitalWrite(IN3, HIGH);
    digitalWrite(IN4, LOW);
  }
  else {
    digitalWrite(IN3, LOW);
    digitalWrite(IN4, HIGH);
  }
}