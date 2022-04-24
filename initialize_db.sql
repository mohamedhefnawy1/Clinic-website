CREATE TABLE Clinic (
	c_location varchar(255) PRIMARY KEY,
    c_name varchar(255)
);

INSERT INTO Clinic VALUES ("Oakland", "Oakland Clinic");
INSERT INTO Clinic VALUES ("Baltimore", "Baltimore Medical Clinic");

CREATE TABLE Person (
	SSN varchar(255) PRIMARY KEY,
    phone_no varchar(255),
    address varchar(255),
    sex varchar(255)
);

INSERT INTO Person VALUES ("398559017", "587-555-1234", "23 Ridiculous Ave", "Male");
INSERT INTO Person VALUES ("234804548", "587-555-4321", "754 Fantastic St", "Female");
INSERT INTO Person VALUES ("092385880", "587-555-7890", "57 Phantasm Dr", "Female");

CREATE TABLE P_name (
	SSN varchar(255),
    f_name varchar(255),
    l_name varchar(255),
    PRIMARY KEY (SSN, f_name, l_name),
    FOREIGN KEY (SSN) REFERENCES Person(SSN)
);

INSERT INTO P_name VALUES ("398559017", "George", "Redness");
INSERT INTO P_name VALUES ("234804548", "Deborah", "Lynwood");
INSERT INTO P_name VALUES ("092385880", "Diane", "Kindness");

CREATE TABLE DOB (
	SSN varchar(255),
    dob_day int,
    dob_month int,
    dob_year int,
    PRIMARY KEY (SSN, dob_day, dob_month, dob_year),
    FOREIGN KEY (SSN) REFERENCES Person(SSN)
);

INSERT INTO DOB VALUES ("398559017", 12, 21, 1978);
INSERT INTO DOB VALUES ("234804548", 5, 3, 1989);
INSERT INTO DOB VALUES ("092385880", 7, 15, 1969);

CREATE TABLE Doctor (
	SSN varchar(255) PRIMARY KEY,
    field varchar(255),
    c_location varchar(255),
    FOREIGN KEY (SSN) REFERENCES Person(SSN),
    FOREIGN KEY (c_location) REFERENCES Clinic(c_location)
);

INSERT INTO Doctor VALUES ("398559017", "Nephrology", "Oakland");
INSERT INTO Doctor VALUES ("234804548", "Infectious diseases", "Oakland");
INSERT INTO Doctor VALUES ("092385880", "Psychiatry", "Baltimore");

CREATE TABLE Educational_bg (
	d_ssn varchar(255),
    school varchar(255),
    years int,
    PRIMARY KEY (d_ssn, school, years),
    FOREIGN KEY (d_ssn) REFERENCES Doctor(SSN)
);

INSERT INTO Educational_bg VALUES ("398559017", "Harvard", 5);
INSERT INTO Educational_bg VALUES ("234804548", "Yale", 6);
INSERT INTO Educational_bg VALUES ("092385880", "Cambridge", 7);

CREATE TABLE Patient (
	SSN varchar(255) PRIMARY KEY,
    FOREIGN KEY (SSN) REFERENCES Person(SSN)
);

CREATE TABLE Medication (
	m_name varchar(255) PRIMARY KEY,
    side_effects varchar(255)
);

CREATE TABLE EMR (
	p_ssn varchar(255),
    emr_id varchar(255),
    PRIMARY KEY (p_ssn, emr_id),
    FOREIGN KEY (p_ssn) REFERENCES Patient(SSN)
);



CREATE TABLE Symptom (
    s_description varchar(255) PRIMARY KEY,
    body_area varchar(255)
);

CREATE TABLE Diagnosis (
	d_ssn varchar(255),
    diagnosis_no int UNIQUE,
    s_description varchar(255),
    illness varchar(255),
    p_ssn varchar(255),
    emr_id varchar(255),
    diag_day int,
    diag_month int,
    diag_year int,
    PRIMARY KEY (p_ssn, emr_id, diagnosis_no),
    FOREIGN KEY (d_ssn) REFERENCES Doctor(SSN),
    FOREIGN KEY (p_ssn, emr_id) REFERENCES EMR(p_ssn, emr_id),
	FOREIGN KEY (s_description) REFERENCES Symptom(s_description)
);

ALTER TABLE `Diagnosis` MODIFY COLUMN `Diagnosis_no` int AUTO_INCREMENT;

CREATE TABLE Prescription (
    diagnosis_no int UNIQUE,
	d_ssn varchar(255),
    m_name varchar(255),
    dosage varchar(255),
    p_ssn varchar(255),
    emr_id varchar(255),
    PRIMARY KEY (d_ssn, m_name, p_ssn),
    FOREIGN KEY (d_ssn) REFERENCES Doctor(SSN),
    FOREIGN KEY (diagnosis_no) REFERENCES Diagnosis(diagnosis_no),
    FOREIGN KEY (m_name) REFERENCES Medication(m_name),
    FOREIGN KEY (p_ssn, emr_id) REFERENCES EMR(p_ssn, emr_id)
);
CREATE TABLE Treats (
	m_name varchar(255),
    s_description varchar(255),
    PRIMARY KEY (m_name, s_description),
    FOREIGN KEY (m_name) REFERENCES Medication(m_name),
    FOREIGN KEY (s_description) REFERENCES Symptom(s_description)
);

-- creating fake medication and prescription, diagnosis, symtpoms, etc
INSERT INTO Medication VALUES ("Tylenol", "nausea");
INSERT INTO Symptom VALUES ("headache", "head");
INSERT INTO Treats VALUES ("Tylenol", "headache");