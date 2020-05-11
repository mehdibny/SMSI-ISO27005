using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSI_ISO27005.Models;
using SMSI_ISO27005.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SMSI_ISO27005.ViewModels
{
    public class CIDActifVM
    {
        public CID_actif CIDDetailles { get; set; }
        public actif actifDetailles { get; set; }
        public activite activiteDetaillese { get; set; }
        public confid confidDetailles { get; set; }
        public integrite integriteDetailles { get; set; }
        public disponibilte disponibilteDetailles { get; set; }
        public collaborateur collaborateurDetailles { get; set; }
        public user_table user_tableDetailles { get; set; }
        public vulnerabilte vulnerabilteDetailles { get; set; }
        public menace menaceDetailles { get; set; }
        public prob_occurrence probOccurrenceDetailles { get; set; }
        public impact impactDetailles { get; set; }
        public gestion_risque gestionDetailles { get; set; }
        public action actionDetailles { get; set; }
        public ActionMusereViewModel ActionMuseres { get; set; }
    }
}