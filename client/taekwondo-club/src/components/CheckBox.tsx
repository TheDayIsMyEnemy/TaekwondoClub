import React, { useState, useEffect } from "react";
import "../css/CheckBox.css";

const CheckBox = () => {
  return (
    <div className="form-page">
      <div className="form">
        <div className="checkbox" id="cb1">
          <input id="check1" type="checkbox" />
          <label></label>
        </div>
       
      </div>
    </div>
  );
};

export default CheckBox;
