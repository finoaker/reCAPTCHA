﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Finoaker.Web.Recaptcha
{
    /// <summary>
    /// Contains reCAPTCHA settings from configuration.
    /// </summary>
    [Serializable]
    public class RecaptchaSettings
    {
        /// <summary>
        /// <see cref="List{Key}"/> of keys for the various reCAPTCHA types. Keys can be obtained from reCAPTCHA admin console.
        /// </summary>
        [Required]
        public List<Key> Keys { get; set; }

        /// <summary>
        /// Returns the first <see cref="Key"/> of the sequence that matches <see cref="RecaptchaType"/> or null if no such element is found.
        /// </summary>
        public Key First(RecaptchaType type)
        {
            return Keys?.FirstOrDefault(key => key.KeyType == type);
        }
    }

    /// <summary>
    /// Contains site and secret keys of a specific <see cref="RecaptchaType"/>
    /// </summary>
    [Serializable]
    public class Key
    {
        /// <summary>
        /// Default minimum score. 
        /// </summary>
        public const decimal DefaultMinimumScore = 0.8m;
        /// <summary>
        /// reCAPTCHA Secret Key
        /// </summary>
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string SecretKey { get; set; }

        /// <summary>
        /// reCAPTCHA Site Key
        /// </summary>
        [StringLength(40, MinimumLength = 40)]
        public string SiteKey { get; set; }

        /// <summary>
        /// Type of reCAPTCHA service the keys can be used with (eg. v2 checkbox)
        /// </summary>
        [Required]
        public RecaptchaType KeyType { get; set; }

        /// <summary>
        /// [v3 only] Minimum score required to successfully pass verification. Score must be between 0.0 and 1.0. (Default 0.8)
        /// </summary>
        public decimal? MinimumScore {
            get
            {
                if (KeyType == RecaptchaType.V3)
                {
                    return _MinimumScore ?? DefaultMinimumScore;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (KeyType == RecaptchaType.V3 && value >= 0.0m && value <= 1.0m)
                {
                    _MinimumScore = value;
                }
            }
        }
        private decimal? _MinimumScore;
    }
}
